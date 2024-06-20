using BankAccountAPI.Data;
using BankAccountAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;

namespace BankAccountAPI.Repository
{
	public class BankAccountRepository : IBankAccountRepository
	{
		private readonly ApiContext _apiContext;
        
        public BankAccountRepository()
		{
			_apiContext = new ApiContext();
			if (!_apiContext.BankAccounts.Any())
			{
				var bankAccounts = new List<BankAccount>
				{
					new BankAccount { BankAccountNumber = "1234567890", BankAccountAmount = 1000 },
					new BankAccount { BankAccountNumber = "0987654321", BankAccountAmount = 2000 }
				};

				_apiContext.BankAccounts.AddRange(bankAccounts);
				_apiContext.SaveChanges();
			}
		}

		public List<BankAccount> GetAllAccounts()
		{
			return _apiContext.BankAccounts.ToList();
		}
		public BankAccount GetAccountById(int accountId)
		{

			return _apiContext.BankAccounts.FirstOrDefault(a => a.BankAccountId == accountId);
		}

		public BankAccount GetAccountByNumber(string accountNumber)
		{
			return _apiContext.BankAccounts.FirstOrDefault(a => a.BankAccountNumber == accountNumber);
		}

		public void CreateAccount(BankAccount account)
		{
			_apiContext.BankAccounts.Add(account);
			_apiContext.SaveChanges();
		}

		public void UpdateAccount(BankAccount account)
		{
			_apiContext.BankAccounts.Update(account);
			_apiContext.SaveChanges();
		}

		public void DeleteAccount(int accountId)
		{
			var account = _apiContext.BankAccounts.FirstOrDefault(a => a.BankAccountId == accountId);
			if (account != null)
			{
				_apiContext.BankAccounts.Remove(account);
				_apiContext.SaveChanges();
			}
		}
	}
}