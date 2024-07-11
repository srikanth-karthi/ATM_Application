using ATM_MiniProject.Context;
using ATMMiniproject.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMMiniproject.Repository
{
    public class AccountRepository : CRUDRepository<int,Account>,IAccountRepository
    {
        public AccountRepository(ATMContext context) : base(context) { }
        
        public override async Task<IEnumerable<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }

        public override async Task<Account> GetbyId(int key)
        {
            try
            {
                var res = await _context.Accounts.SingleOrDefaultAsync(item => item.AcctId == key);
                return res;
            }catch(Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
            }
            return null;
        }
    }
}
