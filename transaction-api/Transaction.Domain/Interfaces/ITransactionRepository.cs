using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Entities;

namespace Transaction.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<bool> SaveTransaction(Entities.Transaction transfer);
        Task<bool> UpdateStatusTransaction(Guid Id, string  status);
        Task<Entities.Transaction?> GetTransactionByIdAndDate(Guid id, DateTime date);
    }
}
