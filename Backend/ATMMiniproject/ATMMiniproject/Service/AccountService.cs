using ATMMiniproject.Repository.Interfaces;
using ATMMiniproject.Service.Interfaces;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ATMMiniproject.Service
{
    public class AccountService
    {
        private readonly IDebitCardDetailsRepository _cardRepo;
        private readonly ITokenService _tokenService;

        public AccountService(IDebitCardDetailsRepository cardRepo , ITokenService tokenService)
        {
            _cardRepo = cardRepo;
            _tokenService = tokenService;
        }

        public async Task<string> Login(int id,string pin)
        {
           var card= await _cardRepo.GetbyId(id);

          using(HMACSHA512 hmacsha512 = new HMACSHA512(card.HashSalt))
            {
                var encryptedpin = hmacsha512.ComputeHash(Encoding.UTF8.GetBytes(pin.ToString()));
                if (_tokenService.VerifyPassword(card.PinHashed, encryptedpin))
                {
                    var token = _tokenService.GenerateToken(card.CardId);
                    return token;
                }
            }
            throw new UnauthorizedAccessException("Please Check pin");
        }

    }
}
