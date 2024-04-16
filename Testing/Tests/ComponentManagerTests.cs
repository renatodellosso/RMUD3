using RMUD3.Server;

namespace Testing.Tests
{
	[TestClass]
	public class ComponentManagerTests
	{
		private ComponentManager? componentManager;

		[TestInitialize]
		public void Initialize()
		{
			componentManager = new ComponentManager();
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void SetSession_SessionAlreadySet_Exception()
		{
			var session = new Session(null, componentManager!);
			componentManager!.Session = session;
			componentManager!.Session = null;
			Assert.IsNull(componentManager!.Session);
		}
	}
}