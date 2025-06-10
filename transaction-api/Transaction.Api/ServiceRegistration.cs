using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Application.Commands;
using Transaction.Application.Common.Messaging;
using Transaction.Domain.Interfaces;
using Transaction.Infrastructure.Messaging;
using Transaction.Infrastructure.Persistence;
using Transaction.Infrastructure.Repositories;

namespace Transaction.Api
{
    public static class ServiceRegistration
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SaveTransactionCommand).Assembly));
           
        }

        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TransactionDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IKafkaProducer>(sp => new KafkaProducer(configuration["Kafka:BootstrapServers"]));

            services.AddHostedService<KafkaConsumer>();

            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
