using Microsoft.AspNetCore.SignalR;
using RMUD3.Server.SignalR;

namespace RMUD3.Server
{
	public class ClientCommunicationManager(string userId)
	{

		private readonly string userId = userId;

		private IHubContext<MiddlewareHub, IMiddlewareHubClient>? hubContext;

		public IMiddlewareHubClient? Client => GetHubContext()?.Clients.User(userId);

		public IHubContext<MiddlewareHub, IMiddlewareHubClient>? GetHubContext()
		{
			hubContext ??= Services.HubContext;
			return hubContext;
		}
	}
}
