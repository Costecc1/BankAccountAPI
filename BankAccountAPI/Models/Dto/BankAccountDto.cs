using System.ComponentModel.DataAnnotations;

namespace BankAccountAPI.Models.Dto;

public class BankAccountDto
{
	public int BankAccountId { get; set; }
	public string BankAccountNumber { get; set; }
	public decimal BankAccountAmount { get; set; }
}