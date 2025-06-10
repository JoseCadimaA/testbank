using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Domain.Interfaces
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(string topic, T message);
    }
}
