using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Transaction.Infrastructure.Persistence
{
    public class TransactionDbContextFactory : IDesignTimeDbContextFactory<TransactionDbContext>
    {
        public TransactionDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Transaction.Api")) // esto depende de dónde ejecutes el comando
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TransactionDbContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            return new TransactionDbContext(optionsBuilder.Options);
        }
    }
}
