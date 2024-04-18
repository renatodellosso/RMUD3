using RMUD3.Server;

namespace Testing
{
	internal class TestUtils
	{
		public static IEnvService GenerateEnv(string content)
		{
			// Generate a temp file with sample values
			var tempFile = Path.GetTempFileName();
			File.WriteAllText(tempFile, content);

			IEnvService envService = new EnvService(tempFile);

			File.Delete(tempFile);

			return envService;
		}
	}
}
