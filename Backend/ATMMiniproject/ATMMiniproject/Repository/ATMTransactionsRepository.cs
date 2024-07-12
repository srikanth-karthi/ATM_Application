using ATM_MiniProject.Context;
using ATM_MiniProject.Models;
using ATMMiniproject.Exceptions;
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
          
                var res = await _context.ATMTransactions.SingleOrDefaultAsync(item => item.TransactionId == key);
            if (res == null) throw new NoSuchIteminDbException("No Such Item in Db");
            return res;

        }
        public async Task<IList<ATMTransaction>> GetAllByAcctId(int AcctId)
        {
            var res = await _context.ATMTransactions.Where(i => i.AcctId == AcctId).ToListAsync();
            return res;
        }
    }
}
