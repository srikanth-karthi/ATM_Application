using ATM_MiniProject.Models;
using System.ComponentModel.DataAnnotations;

namespace ATM_MiniProject.Context
{
    public class Account
    {
        [Key]
        public int AcctId { get; set; }    
        public float Balance { get; set; }
         
        public IEnumerable<ATMTransaction> Transactions { get; set; } 
        public DebitCardDetails DebitCardDetails { get; set; }
    }
}
