namespace AntiFraud.Infrastructure.Storage;

public static class RedisFraudKeys
{
    public const string IndividualLimit = "antifraud:limits:individual";
    public const string AccumulatedLimit = "antifraud:limits:accumulated-24h";
}
