namespace BankAccountAPI.Utilities
{
	public static class Message
	{
		public static string ErrorGettingBankAccounts = "Error getting bank accounts.";
		public static string ErrorGettingBankAccountByID(int id) => $"Error getting bank account by ID: {id}.";
		public static string ErrorGettingBankAccountByAccountNumber(string number) => $"Error getting bank account by number: {number}.";
		public static string ErrorCreatingBankAccount = "Error creating bank account.";
		public static string ErrorUpdatingBankAccount = "Error updating bank account.";
		public static string ErrorDeletingBankAccount = "Error deleting bank account.";
	}
}
