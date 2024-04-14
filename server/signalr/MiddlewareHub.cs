using Microsoft.AspNetCore.SignalR;

namespace RMUD3.server.signalr
{
	public interface IMiddlewareHubClient
	{
		Task Receive(string action, object args);
	}

	public class MiddlewareHub : Hub<IMiddlewareHubClient>
	{
		public async Task Send(string action, object args, ISessionManagerService sessionManager)
		{
			await sessionManager.GetSession(Context).HandleClientAction(action, args);
		}

		public override Task OnConnectedAsync()
		{
			Console.WriteLine("Client connected!");
			return Task.CompletedTask;
		}
	}
}