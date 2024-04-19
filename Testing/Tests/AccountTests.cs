using RMUD3.Server;
using System.Reflection;
using System.Text;

namespace Testing.Tests
{
	[TestClass]
	public class AccountTests
	{

		private static readonly byte[] pepper = Encoding.UTF8.GetBytes("pepper");

		private const int PARALLELISM = 4, ITERATIONS = 10;
		private const string MEMORY = "25KB";
		private static IEnvService GenerateEnv(int parallelism = PARALLELISM, int iterations = ITERATIONS, string memory = MEMORY, string? pepper = null)
			=> TestUtils.GenerateEnv($"PARALLELISM={parallelism}\nITERATIONS={iterations}\nMEMORY={memory}\nPEPPER={pepper ?? Convert.ToBase64String(AccountTests.pepper)}");

		private static byte[]? GenerateSalt() => (byte[]?)typeof(Account).GetMethod("GenerateSalt", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, null);
		private static string? Hash(string password, byte[] salt, IEnvService env)
			=> (string?)typeof(Account).GetMethod("Hash", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, [password, salt, env]);

		[TestMethod]
		public void Hash_FakeConfig_Success()
		{
			byte[]? salt = GenerateSalt();
			if (salt == null)
				Assert.Fail("Salt is null");

			string? hash = Hash("password", salt, GenerateEnv());

			if (hash == null)
				Assert.Fail("Hash is null");

			Assert.AreEqual(44, hash.Length);
		}

		[TestMethod]
		public void Hash_MultiDifferentPassword_Success()
		{
			byte[]? salt = GenerateSalt();
			if (salt == null) Assert.Fail("Salt is null");

			// Generate 2 hashes
			IEnvService envService = GenerateEnv();
			string? hash1 = Hash("password1", salt, envService);
			string? hash2 = Hash("password2", salt, envService);

			if (hash1 == null) Assert.Fail("Hash1 is null");
			if (hash2 == null) Assert.Fail("Hash2 is null");

			Assert.AreNotEqual(hash1, hash2);
		}

		[TestMethod]
		public void Hash_MultiDifferentSalt_Success()
		{
			byte[]? salt1 = GenerateSalt();
			if (salt1 == null) Assert.Fail("Salt1 is null");

			byte[]? salt2 = GenerateSalt();
			if (salt2 == null) Assert.Fail("Salt2 is null");

			// Generate 2 hashes
			IEnvService envService = GenerateEnv();
			string? hash1 = Hash("password", salt1, envService);
			string? hash2 = Hash("password", salt2, envService);

			if (hash1 == null) Assert.Fail("Hash1 is null");
			if (hash2 == null) Assert.Fail("Hash2 is null");

			Assert.AreNotEqual(hash1, hash2);
		}

		[TestMethod]
		public void Hash_MultiDifferentPepper_Success()
		{
			byte[]? salt = GenerateSalt();
			if (salt == null) Assert.Fail("Salt is null");

			string? hash1 = Hash("password", salt, GenerateEnv());
			string? hash2 = Hash("password", salt, GenerateEnv(pepper: "pepper2"));

			if (hash1 == null) Assert.Fail("Hash1 is null");
			if (hash2 == null) Assert.Fail("Hash2 is null");

			Assert.AreNotEqual(hash1, hash2);
		}

		[TestMethod]
		public void Hash_MultiDifferentParallelism_Success()
		{
			byte[]? salt = GenerateSalt();
			if (salt == null) Assert.Fail("Salt is null");

			// Generate 2 hashes
			IEnvService envService1 = GenerateEnv();

			IEnvService envService2 = GenerateEnv(PARALLELISM + 1);

			string? hash1 = Hash("password", salt, envService1);
			string? hash2 = Hash("password", salt, envService2);

			if (hash1 == null) Assert.Fail("Hash1 is null");
			if (hash2 == null) Assert.Fail("Hash2 is null");

			Assert.AreNotEqual(hash1, hash2);
		}

		[TestMethod]
		public void Hash_MultiDifferentIterations_Success()
		{
			byte[]? salt = GenerateSalt();
			if (salt == null) Assert.Fail("Salt is null");

			// Generate 2 hashes
			IEnvService envService1 = GenerateEnv();

			IEnvService envService2 = GenerateEnv(iterations: ITERATIONS + 1);

			string? hash1 = Hash("password", salt, envService1);
			string? hash2 = Hash("password", salt, envService2);

			if (hash1 == null) Assert.Fail("Hash1 is null");
			if (hash2 == null) Assert.Fail("Hash2 is null");

			Assert.AreNotEqual(hash1, hash2);
		}

		[TestMethod]
		public void Hash_MultiDifferentMemory_Success()
		{
			byte[]? salt = GenerateSalt();
			if (salt == null) Assert.Fail("Salt is null");

			// Generate 2 hashes
			IEnvService envService1 = GenerateEnv();

			// Increment memory by 1 by parsing the first character as an int and then incrementing it
			IEnvService envService2 = GenerateEnv(memory: $"{int.Parse(MEMORY[0].ToString()) + 1}KB");

			string? hash1 = Hash("password", salt, envService1);
			string? hash2 = Hash("password", salt, envService2);

			if (hash1 == null) Assert.Fail("Hash1 is null");
			if (hash2 == null) Assert.Fail("Hash2 is null");

			Assert.AreNotEqual(hash1, hash2);
		}

		[TestMethod]
		public void Hash_RealEnv_Success()
		{
			byte[]? salt = GenerateSalt();
			if (salt == null) Assert.Fail("Salt is null");

			// Generate 2 hashes
			string filePath = "../../../../.env";
			IEnvService envService = new EnvService(filePath);

			string? hash = Hash("password", salt, envService);

			if (hash == null) Assert.Fail("Hash is null");

			Assert.AreEqual(44, hash.Length);
		}
	}
}
