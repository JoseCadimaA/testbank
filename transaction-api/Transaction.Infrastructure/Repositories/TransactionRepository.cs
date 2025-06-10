using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Interfaces;
using Transaction.Infrastructure.Persistence;

namespace Transaction.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TransactionDbContext _dbContext;

        public TransactionRepository(TransactionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Domain.Entities.Transaction?> GetTransactionByIdAndDate(Guid id, DateTime date)
        {
            var startDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Utc);
            var endDate = startDate.AddDays(1);

            return _dbContext.Transactions
        .Where(t => t.Id == id && t.createAt >= startDate && t.createAt < endDate)
        .SingleOrDefaultAsync();
        }

        public async Task<bool> SaveTransaction(Domain.Entities.Transaction transfer)
        {
            await _dbContext.Transactions.AddAsync(transfer);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateStatusTransaction(Guid Id, string status)
        {
            var transaction = await _dbContext.Transactions.FindAsync(Id);

            if (transaction == null)
                return false;

            transaction.status = status;

            _dbContext.Transactions.Update(transaction);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
