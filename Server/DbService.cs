using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace RMUD3.Server
{
	public interface IDbService
	{
		public DbSet<Account> Accounts { get; init; }

		/// <summary>
		/// Save changes to the database.
		/// </summary>
		/// <returns>The number of entries changed.</returns>
		public int SaveChanges();
	}

	public class DbService : DbContext, IDbService
	{

		public DbSet<Account> Accounts { get; init; }

		public DbService(IEnvService env) : base(new DbContextOptionsBuilder().UseMongoDB(new MongoClient(env.MongoDbUri), env.DbName).Options)
		{
			Console.WriteLine("DbService initialized");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			var account = modelBuilder.Entity<Account>();
			account.Property("salt");
			account.ToCollection("accounts");
		}

	}

}
