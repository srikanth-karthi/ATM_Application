using ATM_MiniProject.Context;
using ATM_MiniProject.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATM_MiniProject.Models
{
    public class ATMTransaction
    {
        [Key]
        public int TransactionId { get; set; } 
        public int AcctId { get; set; }
        public int Amount { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime Date { get; set; }
        public int Balance { get; set; }
    }
}
