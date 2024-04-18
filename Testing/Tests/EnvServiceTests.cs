using System.Text;

namespace Testing.Tests
{
	[TestClass]
	public class EnvServiceTests
	{
		[TestMethod]
		public void GetMongoDbUri_Present_Success()
		{
			var envService = TestUtils.GenerateEnv("MONGODB_URI=mongodb://localhost:27017");
			Assert.AreEqual("mongodb://localhost:27017", envService.MongoDbUri);
		}

		[TestMethod]
		[ExpectedException(typeof(KeyNotFoundException))]
		public void GetMongoDbUri_NotPresent_Exception()
		{
			var envService = TestUtils.GenerateEnv("");
			Assert.AreEqual(Encoding.UTF8.GetBytes(""), envService.Pepper);
		}

		[TestMethod]
		public void GetPepper_Present_Success()
		{
			var envService = TestUtils.GenerateEnv("PEPPER=pepper");
			Assert.AreEqual("pepper", Encoding.UTF8.GetString(envService.Pepper ?? []));
		}

		[TestMethod]
		public void GetParallelism_Present_Success()
		{
			var envService = TestUtils.GenerateEnv("PARALLELISM=4");
			Assert.AreEqual(4, envService.Parallelism);
		}

		[TestMethod]
		public void GetIterations_Present_Success()
		{
			var envService = TestUtils.GenerateEnv("ITERATIONS=4");
			Assert.AreEqual(4, envService.Iterations);
		}

		[TestMethod]
		public void GetMemory_Present_Success()
		{
			var envService = TestUtils.GenerateEnv("MEMORY=4KB");
			Assert.AreEqual(4 * 1024, envService.Memory);

			envService = TestUtils.GenerateEnv("MEMORY=4MB");
			Assert.AreEqual(4 * 1024 * 1024, envService.Memory);

			envService = TestUtils.GenerateEnv("MEMORY=4");
			Assert.AreEqual(4, envService.Memory);
		}
	}
}
