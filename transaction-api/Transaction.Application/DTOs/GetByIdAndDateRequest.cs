using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.DTOs
{
    public class GetByIdAndDateRequest
    {
        public Guid transactionId { get; set; }
        public DateTime createdAt { get; set; }
    }
}
