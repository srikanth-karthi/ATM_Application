using ATM_MiniProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

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
            modelBuilder.Entity<Account>().HasData(
               new Account()
               {
                   AcctId = 123456,
                   Balance = 1000
               }
           );
            using (HMACSHA512 hmacsha512 = new HMACSHA512())
            {
                modelBuilder.Entity<DebitCardDetails>().HasData(

                new DebitCardDetails()
                {
                    CardId = 1,
                    PinHashed = hmacsha512.ComputeHash(Encoding.UTF8.GetBytes("1234")),
                    HashSalt = hmacsha512.Key,
                    AcctId = 123456,
                }
               );
            }


           
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
