using ATM_MiniProject.Models;

namespace ATMMiniproject.Service.Interfaces
{
    public interface IAccountService
    {
            Task<string> Login(int id, string pin); 
            Task<ATMTransaction> Withdraw(int amount);
            Task<ATMTransaction> Deposit(int amount);
            Task<int> GetAccountBalance();
        Task<IList<ATMTransaction>> GetAllTransaction();
    }
}
