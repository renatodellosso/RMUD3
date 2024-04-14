using Microsoft.AspNetCore.SignalR;
using RMUD3.server.signalr;

namespace RMUD3.server
{
	public class Session
	{

		private readonly string userId;

		private IHubContext<MiddlewareHub, IMiddlewareHubClient> hubContext;

		private IMiddlewareHubClient Client => hubContext.Clients.User(userId);

		public Session(string userId)
		{
			hubContext = Services.GetHubContext();

			this.userId = userId;
		}

		public async Task HandleClientAction(string action, object args)
		{
			await Client.Receive(action, args);
		}

	}
}
