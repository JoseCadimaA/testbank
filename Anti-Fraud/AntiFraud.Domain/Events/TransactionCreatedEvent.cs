using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Domain.Events
{
    public class TransactionCreatedEvent
    {
        public Guid TransactionId { get; set; }
        public Guid SourceAccountId { get; set; }
        public Guid TargetAccountId { get; set; }
        public int TransferTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
