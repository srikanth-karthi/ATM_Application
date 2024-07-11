using ATMMiniproject.Repository.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace ATMMiniproject.Service
{
    public class AccountService
    {
        private readonly IDebitCardDetailsRepository _cardRepo;

        public AccountService(IDebitCardDetailsRepository cardRepo)
        {
            _cardRepo = cardRepo;
        }

        public async Task<string> login(int id,string pin)
        {
           var card= await _cardRepo.GetbyId(id);

          using(HMACSHA512 hmacsha512 = new HMACSHA512(card.HashSalt))
            {
                var encryptedpin=HMACSHA512.ComputeHash(Encoding.UTF8.GetBytes(pin);



            }
        }
    }
}
