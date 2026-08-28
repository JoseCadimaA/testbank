using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.Globalization;

namespace AntiFraud.Infrastructure.Storage;

public class RedisFraudLimitInitializer : IHostedService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IConfiguration _configuration;

    public RedisFraudLimitInitializer(
        IConnectionMultiplexer redis,
        IConfiguration configuration)
    {
        _redis = redis;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var individualLimit = _configuration.GetValue<decimal>("FraudLimits:Individual", 2000m);
        var accumulatedLimit = _configuration.GetValue<decimal>("FraudLimits:Accumulated24Hours", 20000m);
        var database = _redis.GetDatabase();

        await database.StringSetAsync(
            RedisFraudKeys.IndividualLimit,
            individualLimit.ToString(CultureInfo.InvariantCulture),
            when: When.NotExists);
        await database.StringSetAsync(
            RedisFraudKeys.AccumulatedLimit,
            accumulatedLimit.ToString(CultureInfo.InvariantCulture),
            when: When.NotExists);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
