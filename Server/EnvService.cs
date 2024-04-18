using EnvDotNet;
using System.Text;

namespace RMUD3.Server
{
	public interface IEnvService
	{
		public string MongoDbUri { get; }

		public byte[] Pepper { get; }
		public int Parallelism { get; }
		public int Iterations { get; }
		public int Memory { get; }
	}

	public class EnvService : IEnvService
	{

		private Env env;

		public EnvService(string? filepath = null)
		{
			env = filepath != null ? new Env(filepath) : new Env();
		}

		public string MongoDbUri => env["MONGODB_URI"];

		public byte[] Pepper => Encoding.UTF8.GetBytes(env["PEPPER"]);
		public int Parallelism => int.Parse(env["PARALLELISM"]);
		public int Iterations => int.Parse(env["ITERATIONS"]);
		public int Memory
		{
			get
			{
				string memory = env["MEMORY"];

				if (memory.EndsWith("KB"))
				{
					return int.Parse(memory[..^2]) * 1024;
				}
				else if (memory.EndsWith("MB"))
				{
					return int.Parse(memory[..^2]) * 1024 * 1024;
				}
				else
				{
					return int.Parse(memory);
				}
			}
		}

	}
}
