using ATM_MiniProject.Context;
using ATM_MiniProject.Models;
using ATMMiniproject.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMMiniproject.Repository
{
    public class ATMTransactionsRepository : CRUDRepository<int, ATMTransaction> , IATMTransactionsRepository
    {
        public ATMTransactionsRepository(ATMContext context) : base(context) { }
        public override async Task<IEnumerable<ATMTransaction>> GetAll()
        {
            return await _context.ATMTransactions.ToListAsync();
        }

        public override async Task<ATMTransaction> GetbyId(int key)
        {
            try
            {
                var res = await _context.ATMTransactions.SingleOrDefaultAsync(item => item.TransactionId == key);
                return res;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            return null;
        }
    }
}
