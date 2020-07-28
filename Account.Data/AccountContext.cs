using Account.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Account.Data
{
    public class AccountContext:DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Entities.Account> Accounts { get; set; }
        public virtual DbSet<EmailVerification> EmailVerifications { get; set; }
        public virtual DbSet<OperationHistory> OperationsHistory { get; set; }


        public AccountContext()
        {
        }

        public AccountContext(DbContextOptions<AccountContext> options)
          : base(options)
        {

        }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().HasIndex(x => new { x.Email }).IsUnique();
            modelBuilder.Entity<Entities.Account>().Property(x => x.Balance).HasDefaultValue(1000000);
        }
    }
}
