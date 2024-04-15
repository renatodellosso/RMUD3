namespace RMUD3.Server
{

	public interface ISession
	{

	}


	public class Session : ISession
	{

		private readonly IClientCommunicationManager clientCommunicationHandler;
		public IComponentManager ComponentManager { get; }

		public Session(IClientCommunicationManager clientCommunicationHandler, IComponentManager componentManager)
		{
			this.clientCommunicationHandler = clientCommunicationHandler;
			ComponentManager = componentManager;
			ComponentManager.Session = this;
		}
	}
}
