using BankAccountAPI.Models;

namespace BankAccountAPI.Repository
{
	public interface IBankAccountRepository
	{
		public List<BankAccount> GetAllAccounts();
		public BankAccount GetAccountById(int accountId);
		public BankAccount GetAccountByNumber(string accountNumber);
		public void CreateAccount(BankAccount account);
		public void UpdateAccount(BankAccount account);
		public void DeleteAccount(int accountId);
	}
}
