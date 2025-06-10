using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Application.DTOs
{
    public class SaveTransactionRequest
    {
        public Guid sourceAccountId { get; set; }
        public Guid targetAccountId { get; set; }
        public int transferTypeId { get; set; }
        public decimal value { get; set; }
    }
}
