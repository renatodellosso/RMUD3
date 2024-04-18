using MongoDB.Driver;

namespace RMUD3.Server
{
	public interface IDbService
	{
	}

	public class DbService : IDbService
	{

		private MongoClient client;

		public DbService(IEnvService env)
		{
			client = new(env.MongoDbUri);
		}

	}
}
