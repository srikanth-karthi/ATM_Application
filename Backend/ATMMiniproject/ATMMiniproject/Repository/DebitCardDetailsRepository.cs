using ATM_MiniProject.Context;
using ATMMiniproject.Exceptions;
using ATMMiniproject.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ATMMiniproject.Repository
{
    public class DebitCardDetailsRepository : CRUDRepository<int, DebitCardDetails>, IDebitCardDetailsRepository
    {
        public DebitCardDetailsRepository(ATMContext context) : base(context) { }
        public override async Task<IEnumerable<DebitCardDetails>> GetAll()
        {
            return await _context.DebitCardDetails.ToListAsync();
        }

        public override async Task<DebitCardDetails> GetbyId(int key)
        {

                var res = await _context.DebitCardDetails.Include(item => item.Account).FirstOrDefaultAsync(item => item.CardId == key);
                if (res == null) throw new NoSuchIteminDbException("No Such Item in Db");
                return res;
            
        }
       
    }
}
