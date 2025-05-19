using AntiFraud.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntiFraud.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> ValidateAmountTransaction(Guid TransactionId, decimal amount);
        Task<bool> AddOrdenAch(OrdenACH ordenACH);
        Task<decimal> GetTotalTransferBySourceAccount(Guid sourceAccountId);
    }
}
