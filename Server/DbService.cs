using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using RMUD3.Server.Gameplay;

namespace RMUD3.Server
{
	public interface IDbService
	{
		public DbSet<Account> Accounts { get; init; }
		public DbSet<Player> Players { get; init; }

		/// <summary>
		/// Save changes to the database.
		/// </summary>
		/// <returns>The number of entries changed.</returns>
		public int SaveChanges();
	}

	public class DbService : DbContext, IDbService
	{

		public DbSet<Account> Accounts { get; init; }
		public DbSet<Player> Players { get; init; }

		public DbService(IEnvService env) : base(new DbContextOptionsBuilder().UseMongoDB(new MongoClient(env.MongoDbUri), env.DbName).Options)
		{
			Console.WriteLine("DbService initialized");
		}

		/// <summary>
		/// Use this method to register polymorphic types to Mongo.
		/// <see href="https://stackoverflow.com/a/43312423/22099600"/>
		/// </summary>
		private static void RegisterClassMaps()
		{
			BsonClassMap.RegisterClassMap<List<ObjectId>>();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			RegisterClassMaps();

			var account = modelBuilder.Entity<Account>();
			account.Property("salt");
			account.ToCollection("accounts");

			var player = modelBuilder.Entity<Player>();
			player.ToCollection("players");
		}

	}

}
