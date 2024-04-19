using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace RMUD3.Server
{
	public class Account : SignInCredentials
	{

		private readonly byte[] salt;

		public Account(string username, string password) : base(username, password)
		{
			salt = GenerateSalt();
		}

		private static string Hash(string password, byte[] salt, IEnvService? envService = null)
		{
			envService ??= Services.GetEnv();

			ArgumentNullException.ThrowIfNull(envService);

			Argon2id argon = new(Encoding.UTF8.GetBytes(password))
			{
				Salt = salt,
				KnownSecret = envService.Pepper,

				DegreeOfParallelism = envService.Parallelism,
				MemorySize = envService.Memory,
				Iterations = envService.Iterations
			};

			return Convert.ToBase64String(argon.GetBytes(32));
		}

		private static byte[] GenerateSalt()
		{
			var buffer = new byte[32];
			RandomNumberGenerator.Create().GetNonZeroBytes(buffer);

			return buffer;
		}
	}
}
