using Microsoft.AspNet.SignalR;

namespace RMUD3.server
{
	public interface IMiddlewareHubClient
	{
		Task Receive(string user, string message);
	}

	public class MiddlewareHub : Hub<IMiddlewareHubClient>
	{
		public async Task Send(string user, string message)
		{
			await Clients.All.Receive(user, message);
		}
	}
}
