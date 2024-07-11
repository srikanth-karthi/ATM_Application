using ATM_MiniProject.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ATM_MiniProject.Context
{
    public class Account
    {
        [Key]
        public int AcctId { get; set; }
        public int Balance { get; set; }
         
        public IEnumerable<ATMTransaction> Transactions { get; set; } = new List<ATMTransaction>();
        public DebitCardDetails DebitCardDetails { get; set; }
    }
}
