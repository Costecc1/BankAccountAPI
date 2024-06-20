using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Models
{
    public class BankAccount
    {
        [Key]
        public int BankAccountId { get; set; }
        [Required]
        public string BankAccountNumber { get; set; }
        [Required]
        public decimal BankAccountAmount { get; set; }
    }
}