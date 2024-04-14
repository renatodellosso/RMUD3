using Microsoft.AspNetCore.SignalR;

namespace RMUD3.server
{
	public interface IMiddlewareHubClient
	{
		Task Receive(string action, object args);
	}

	public class MiddlewareHub(ISessionManagerService sessionManager) : Hub<IMiddlewareHubClient>
	{
		public async Task Send(string action, object args, ISessionManagerService sessionManager)
		{
			await Clients.All.Receive(action, args);
		}

		public override Task OnConnectedAsync()
		{
			if (Context.User != null)
				sessionManager.CreateSession(Context.User);
			Console.WriteLine("Client connected!");
			return Task.CompletedTask;
		}
	}
}
