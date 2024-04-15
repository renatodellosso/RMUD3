using Microsoft.AspNetCore.SignalR;
using RMUD3.Server.SignalR;

namespace RMUD3.Server
{

	public interface IClientCommunicationManager
	{
		IHubContext<MiddlewareHub, IMiddlewareHubClient> GetHubContext();
		IMiddlewareHubClient Client { get; }
	}

	public class ClientCommunicationManager(string userId) : IClientCommunicationManager
	{

		private readonly string userId = userId;

		private IHubContext<MiddlewareHub, IMiddlewareHubClient>? hubContext;

		public IMiddlewareHubClient Client => GetHubContext().Clients.User(userId);

		public IHubContext<MiddlewareHub, IMiddlewareHubClient> GetHubContext()
		{
			hubContext ??= Services.GetHubContext();
			return hubContext;
		}
	}
}
