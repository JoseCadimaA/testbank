using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transaction.Domain.ValueObjects
{
    public class Amount
    {
        public decimal Value { get; set; }

        public Amount(decimal value) {
            if (!IsValid(value))
                throw new ArgumentException("El monto debe ser mayor a 0.");
            Value = value;
        }

        public static bool IsValid(decimal ammount)
        {
            return ammount > 0;
        }

    }
}
