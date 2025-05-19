using AntiFraud.Domain.Entities;
using AntiFraud.Domain.Interfaces;
using AntiFraud.Infrastructure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task<bool> AddOrdenAch(OrdenACH ordenACH )
        {
            OrdenesAchStore.Add(ordenACH);

            return Task.FromResult(true);
        }

        public Task<decimal> GetTotalTransferBySourceAccount(Guid sourceAccountId)
        {
            return Task.FromResult(OrdenesAchStore.GetTotalTransferBySourceAccount(sourceAccountId));
        }

        public Task<bool> ValidateAmountTransaction(Guid TransactionId, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
