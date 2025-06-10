using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Domain.Enums
{
    public static class TransactionStatus
    {
        public const string Pending = "P";
        public const string Approved = "A";
        public const string REjected = "R";
    }
}
