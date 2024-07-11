using ATM_MiniProject.Models;

namespace ATMMiniproject.Service.Interfaces
{
    public interface IAccountService
    {
        Task<string> Login(int id, string pin); 
       Task<ATMTransaction> Withdraw(float amount); 
        Task<ATMTransaction> Deposit(float amount);
    }
}
