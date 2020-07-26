using Microsoft.EntityFrameworkCore;

namespace Transaction.Data
{
    public class TransactionContext : DbContext
    {
        public virtual DbSet<Entities.Transaction> Transactions { get; set; }

        public TransactionContext()
        {
        }

        public TransactionContext(DbContextOptions<TransactionContext> options)
          : base(options)
        {

        }
    }
    
}
