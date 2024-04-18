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

		public static string Hash(string password, byte[] salt, byte[] pepper, IEnvService? envService = null)
		{
			envService ??= Services.GetEnv();

			ArgumentNullException.ThrowIfNull(envService);

			Argon2id argon = new(Encoding.UTF8.GetBytes(password));

			argon.Salt = salt;
			argon.KnownSecret = pepper;

			argon.DegreeOfParallelism = envService.Parallelism;
			argon.MemorySize = envService.Memory;
			argon.Iterations = envService.Iterations;

			return Convert.ToBase64String(argon.GetBytes(32));
		}

		public static byte[] GenerateSalt()
		{
			var buffer = new byte[32];
			RandomNumberGenerator.Create().GetNonZeroBytes(buffer);

			return buffer;
		}
	}
}
