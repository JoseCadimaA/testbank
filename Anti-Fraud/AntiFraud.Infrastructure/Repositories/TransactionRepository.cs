using AntiFraud.Domain.Entities;
using AntiFraud.Domain.Interfaces;
using AntiFraud.Infrastructure.Storage;
using StackExchange.Redis;
using System.Globalization;

namespace AntiFraud.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private const string ValidateTransactionScript = """
        local individualLimit = tonumber(redis.call('GET', KEYS[1]))
        local accumulatedLimit = tonumber(redis.call('GET', KEYS[2]))

        if not individualLimit or not accumulatedLimit then
            return redis.error_reply('Fraud limits are not configured')
        end

        local amount = tonumber(ARGV[1])
        local transactionId = ARGV[2]
        local now = tonumber(redis.call('TIME')[1])
        local windowStart = now - 86400

        local expiredTransactions = redis.call('ZRANGEBYSCORE', KEYS[3], '-inf', windowStart)
        if #expiredTransactions > 0 then
            redis.call('HDEL', KEYS[4], unpack(expiredTransactions))
            redis.call('ZREM', KEYS[3], unpack(expiredTransactions))
        end

        if redis.call('HEXISTS', KEYS[4], transactionId) == 1 then
            local currentTotal = 0
            for _, value in ipairs(redis.call('HVALS', KEYS[4])) do
                currentTotal = currentTotal + tonumber(value)
            end
            return {1, tostring(currentTotal), 'duplicate'}
        end

        local accumulated = 0
        for _, value in ipairs(redis.call('HVALS', KEYS[4])) do
            accumulated = accumulated + tonumber(value)
        end

        if amount > individualLimit then
            return {0, tostring(accumulated), 'individual_limit'}
        end

        if accumulated + amount > accumulatedLimit then
            return {0, tostring(accumulated), 'accumulated_limit'}
        end

        redis.call('ZADD', KEYS[3], now, transactionId)
        redis.call('HSET', KEYS[4], transactionId, ARGV[1])
        redis.call('EXPIRE', KEYS[3], 90000)
        redis.call('EXPIRE', KEYS[4], 90000)

        return {1, tostring(accumulated + amount), 'approved'}
        """;

    private readonly IDatabase _database;

    public TransactionRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<TransactionValidationResult> ValidateAndRegisterAsync(
        OrdenACH ordenACH,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var accountId = ordenACH.SourceAccountId.ToString("N");
        RedisKey[] keys =
        [
            RedisFraudKeys.IndividualLimit,
            RedisFraudKeys.AccumulatedLimit,
            $"antifraud:account:{accountId}:transactions",
            $"antifraud:account:{accountId}:amounts"
        ];
        RedisValue[] values =
        [
            ordenACH.Amount.ToString(CultureInfo.InvariantCulture),
            ordenACH.TransactionId.ToString("N")
        ];

        var result = (RedisResult[])(await _database.ScriptEvaluateAsync(
            ValidateTransactionScript,
            keys,
            values))!;

        var isApproved = (long)result[0] == 1;
        var accumulatedAmount = decimal.Parse(
            result[1].ToString(),
            CultureInfo.InvariantCulture);

        return new TransactionValidationResult(
            isApproved,
            accumulatedAmount,
            isApproved ? null : result[2].ToString());
    }
}
