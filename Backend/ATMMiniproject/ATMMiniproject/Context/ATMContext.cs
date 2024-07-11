using ATM_MiniProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace ATM_MiniProject.Context
{
    public class ATMContext : DbContext
    {
        public ATMContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<DebitCardDetails> DebitCardDetails { get; set; }
        public DbSet<ATMTransaction> ATMTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
               .HasOne(a => a.DebitCardDetails)
               .WithOne(ac => ac.Account)
               .HasForeignKey<Account>(ac => ac.AcctId);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne()
                .HasForeignKey(a => a.TransactionId);
        }
      
    }
}
