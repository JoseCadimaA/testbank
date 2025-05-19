using AntiFraud.Application.Commands;
using AntiFraud.Application.DTOs;
using AntiFraud.Domain.Events;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AntiFraud.Infrastructure.Messaging
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public KafkaConsumer(
            ILogger<KafkaConsumer> logger,
            IServiceProvider serviceProvider,
            IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = "antifraud-consumer",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe("transaction-created");

            return Task.Run(async () =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = consumer.Consume(stoppingToken);
                        var json = cr.Message.Value;

                        var evt = JsonSerializer.Deserialize<TransactionCreatedEvent>(json);

                        if (evt == null)
                            continue;

                        using var scope = _serviceProvider.CreateScope();
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                        var request = new ValidateTransactionRequest()
                        {
                            TransactionId = evt.TransactionId,
                            SourceAccountId = evt.SourceAccountId,
                            TargetAccountId = evt.TargetAccountId,
                            TransferTypeId = evt.TransferTypeId,
                            Amount = evt.Amount,
                            CreatedAt = evt.CreatedAt
                        };

                        var command = new ValidateTransactionCommand(request);

                        var response = await mediator.Send(command, stoppingToken);

                        _logger.LogInformation("Status updated from Kafka event: {Response}", response);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error consuming Kafka message");
                    }
                }
            });
        }
    }
}
