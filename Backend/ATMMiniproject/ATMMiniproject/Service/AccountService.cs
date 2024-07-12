using ATM_MiniProject.Models.Enums;
using ATM_MiniProject.Models;
using ATMMiniproject.Repository;
using ATMMiniproject.Repository.Interfaces;
using ATMMiniproject.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using ATMMiniproject.Exceptions;

namespace ATMMiniproject.Service
{
    public class AccountService : IAccountService
    {
        private readonly IDebitCardDetailsRepository _cardRepo;
        private readonly ITokenService _tokenService;
        
        private readonly IAccountRepository _accountRepository;
        private readonly IATMTransactionsRepository _atmTransactionsRepository;

        public AccountService(IDebitCardDetailsRepository cardRepo , ITokenService tokenService,IAccountRepository accountRepository , IATMTransactionsRepository atmTransactionsRepository)
        {
            _cardRepo = cardRepo;
            _tokenService = tokenService;
           
            _accountRepository = accountRepository;
            _atmTransactionsRepository = atmTransactionsRepository;
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
     
        public async Task<ATMTransaction> Withdraw(int amt)
        {
            if (amt >= 10000)
            {
                throw new ExceedAmountException("You Cant Withdraw Amount Greater Than 1000");
            }
            int cardId = _tokenService.GetUidFromToken();
            var card = await _cardRepo.GetbyId(cardId);
            if (card.Account.Balance < amt) throw new ExceedAmountException("You balance is low");
            
            card.Account.Balance -= amt;
            await _accountRepository.Update(card.Account);
            return await _atmTransactionsRepository.Add(new ATMTransaction()
            {
                AcctId = card.AcctId,
                Amount = amt,   
                TransactionType = TransactionType.Credit,
                Date = DateTime.Now,
                Balance = card.Account.Balance
            });

        }
        public async Task<ATMTransaction> Deposit(int depositeAmmount)
        {
            if(depositeAmmount >= 20000)
            {
                throw new ExceedAmountException("Deposite Amount Should be Less than 20000");
            }
            int cardId =  _tokenService.GetUidFromToken();
            var card = await _cardRepo.GetbyId(cardId);

            card.Account.Balance += depositeAmmount;

            await _accountRepository.Update(card.Account);

            return await _atmTransactionsRepository.Add(new ATMTransaction()
            {
                AcctId = card.AcctId,
                Amount = depositeAmmount,
                TransactionType = TransactionType.Credit,
                Date = DateTime.Now,
                Balance = card.Account.Balance
            });
        }
        public async Task<IList<ATMTransaction>> GetAllTransaction()
        {
            int cardId = _tokenService.GetUidFromToken();
            var card =await _cardRepo.GetbyId(cardId);
            var transactions = await _atmTransactionsRepository.GetAllByAcctId(card.Account.AcctId);
            return transactions;
        } 
        public async Task<int> GetAccountBalance()
        {
            var card = await _cardRepo.GetbyId(_tokenService.GetUidFromToken());
            return card.Account.Balance;

        }
    }
}
