using RMUD3.Server.Components;

namespace RMUD3.Server
{
	public class Session
	{

		public ClientCommunicationManager? ClientCommunicationHandler { get; }
		public ComponentManager? ComponentManager { get; }

		public Account? Account { get; set; }

		public Session(ClientCommunicationManager? clientCommunicationHandler, ComponentManager? componentManager)
		{
			ClientCommunicationHandler = clientCommunicationHandler;
			ComponentManager = componentManager;
			if (ComponentManager != null)
			{
				ComponentManager.Session = this;
				ComponentManager.Root = new SignInPageComponent(this);
			}
		}

	}
}
