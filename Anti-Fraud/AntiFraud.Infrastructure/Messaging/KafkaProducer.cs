using AntiFraud.Application.Common.Messaging;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AntiFraud.Infrastructure.Messaging
{
    public class KafkaProducer : IKafkaProducer
    {

        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task PublishAsync<T>(string topic, T message)
        {
            var value = JsonSerializer.Serialize(message);
            await _producer.ProduceAsync(topic, new Message<Null, string> { Value = value });
        }
    }
}
