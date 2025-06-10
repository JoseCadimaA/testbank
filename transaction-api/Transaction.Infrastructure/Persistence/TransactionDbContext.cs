using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Entities;

namespace Transaction.Infrastructure.Persistence
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Transaction> Transactions { get; set; }
    }
}
