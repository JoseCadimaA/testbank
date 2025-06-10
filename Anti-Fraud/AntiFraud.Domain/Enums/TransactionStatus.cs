using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Domain.Enums
{
    public static class TransactionStatus
    {
        public const string Pending = "P";
        public const string Approved = "A";
        public const string Rejected = "R";
    }
}
