using BankAccountAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccountAPI.Data
{
    public class ApiContext : DbContext
    {
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseInMemoryDatabase(databaseName: "AuthorDb");
		}
		public DbSet<BankAccount> BankAccounts { get; set; }
    }
} 