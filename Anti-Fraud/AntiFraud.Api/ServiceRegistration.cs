using AntiFraud.Application.Commands;
using AntiFraud.Application.Common.Messaging;
using AntiFraud.Domain.Interfaces;
using AntiFraud.Infrastructure.Messaging;
using AntiFraud.Infrastructure.Repositories;
using AntiFraud.Infrastructure.Storage;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddSingleton<IKafkaProducer>(sp => new KafkaProducer(configuration["Kafka:BootstrapServers"]));

            services.AddHostedService<KafkaConsumer>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddSingleton<OrdenesAchStore>();
        }
    }
}
