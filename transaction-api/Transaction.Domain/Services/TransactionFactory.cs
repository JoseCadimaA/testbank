using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Entities;
using Transaction.Domain.ValueObjects;

namespace Transaction.Domain.Services
{
    public class TransactionFactory
    {
        public static Transaction.Domain.Entities.Transaction CreateNew(Guid sourceAccountId, Guid targetAccountId, int tranferTypeId, decimal amount) {

            var transactionAmount = new Amount(amount);

            var transaction = new Transaction.Domain.Entities.Transaction() { 
                Id = Guid.NewGuid(),
                sourceAccountId = sourceAccountId,
                targetAccountId = targetAccountId,
                transferTypeId = tranferTypeId,
                amount = transactionAmount.Value,
                createAt = DateTime.UtcNow,
                status = Enums.TransactionStatus.Pending
            };

            return transaction;
        }
    }
}
