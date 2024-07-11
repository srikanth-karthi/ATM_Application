using ATM_MiniProject.Context;
using ATMMiniproject.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                var res = await _context.DebitCardDetails.SingleOrDefaultAsync(item => item.CardId == key);
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
