using System.ComponentModel.DataAnnotations;

namespace ATM_MiniProject.Context
{
    public class DebitCardDetails
    {
        [Key]
        public int CardId { get; set; }
        public int AcctId { get; set; }
        public byte[] PinHashed { get; set; }
        public byte[] HashSalt { get; set; }
        public Account Account { get; set; }   
    }
}
