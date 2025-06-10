using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.DTOs
{
    public class UpdateStatusRequest
    {
        public Guid transactionId { get; set; }
        public string? status { get; set; }
    }
}
