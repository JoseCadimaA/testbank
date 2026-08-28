using AntiFraud.Application.Commands;
using AntiFraud.Application.Common.Messaging;
using AntiFraud.Domain.Interfaces;
using AntiFraud.Infrastructure.Messaging;
using AntiFraud.Infrastructure.Repositories;
using AntiFraud.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace AntiFraud.Api
{
    public static class ServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ValidateTransactionCommand).Assembly);
            });
        }

        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var redisConnectionString = configuration["Redis:ConnectionString"]
                ?? throw new InvalidOperationException("Redis:ConnectionString is not configured.");
            var kafkaBootstrapServers = configuration["Kafka:BootstrapServers"]
                ?? throw new InvalidOperationException("Kafka:BootstrapServers is not configured.");

            services.AddSingleton<IKafkaProducer>(_ => new KafkaProducer(kafkaBootstrapServers));
            services.AddSingleton<IConnectionMultiplexer>(_ =>
                ConnectionMultiplexer.Connect(redisConnectionString));

            services.AddHostedService<KafkaConsumer>();
            services.AddHostedService<RedisFraudLimitInitializer>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
