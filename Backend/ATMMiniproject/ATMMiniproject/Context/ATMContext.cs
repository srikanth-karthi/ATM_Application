using ATM_MiniProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
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
            // Seed Account data
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
                        CardId = 654321,
                        PinHashed = hmacsha512.ComputeHash(Encoding.UTF8.GetBytes("1234")),
                        HashSalt = hmacsha512.Key,
                        AcctId = 123456 
                    }
                );
            }

            // Configure one-to-one relationship between Account and DebitCardDetails
            modelBuilder.Entity<Account>()
                .HasOne(a => a.DebitCardDetails)
                .WithOne(d => d.Account)
                .HasForeignKey<DebitCardDetails>(d => d.AcctId); // Set correct foreign key

            // Configure one-to-many relationship between Account and ATMTransaction
            modelBuilder.Entity<Account>()
                .HasMany(a => a.Transactions)
                .WithOne()
                .HasForeignKey(t => t.AcctId);
        }
    }
}