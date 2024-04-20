using Konscious.Security.Cryptography;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace RMUD3.Server
{
	public class Account : SignInCredentials
	{
		// [Key] sets the primary key
		[Key]
		public ObjectId Id { get; private init; }

		private readonly byte[] salt;

		public Account(string username, string password) : base(username, null)
		{
			Id = new ObjectId();
			salt = GenerateSalt();
			Password = Hash(password, salt);
		}

		private static string Hash(string password, byte[] salt, IEnvService? envService = null)
		{
			envService ??= Services.Env;

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

		public bool VerifyPassword(string password)
		{
			return Password == Hash(password, salt);
		}
	}
}
