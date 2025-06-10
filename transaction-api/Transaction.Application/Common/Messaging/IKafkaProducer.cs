using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.Common.Messaging
{
    public interface IKafkaProducer
    {
        Task PublishAsync<T>(string topic, T message);
    }
}
