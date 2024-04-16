using RMUD3.Server;

namespace Testing.Tests
{
	[TestClass]
	public class SessionManagerServiceTests
	{

		private ISessionManagerService? sessionManagerService;

		[TestInitialize]
		public void Initialize()
		{
			Services.Create(null);
			sessionManagerService = new SessionManagerService();
		}

		[TestMethod]
		public void CreateSession_ValidUserId_Success()
		{
			sessionManagerService!.CreateSession("test");
			var session = sessionManagerService.GetSession("test");
			Assert.IsNotNull(session);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void CreateSession_NullUserId_Exception()
		{
			sessionManagerService!.CreateSession(null);
		}

		[TestMethod]
		public void GetSession_ValidUserId_Success()
		{
			sessionManagerService!.CreateSession("test");
			var session = sessionManagerService.GetSession("test");
			Assert.IsNotNull(session);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void GetSession_NullUserId_Exception()
		{
			sessionManagerService!.GetSession(null);
		}

		[TestMethod]
		[ExpectedException(typeof(Exception))]
		public void GetSession_InvalidUserId_Exception()
		{
			sessionManagerService!.GetSession("invalid");
		}

	}
}
