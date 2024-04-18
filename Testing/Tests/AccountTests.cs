using RMUD3.Server;
using System.Text;

namespace Testing.Tests
{
	[TestClass]
	public class AccountTests
	{
		[TestMethod]
		public void Hash_ValidFakeConfig_Success()
		{
			Account.Hash("password", Account.GenerateSalt(), Encoding.UTF8.GetBytes("pepper"), TestUtils.GenerateEnv("PARALLELISM=4\nITERATIONS=4\nMEMORY=4KB"));
		}
	}
}
