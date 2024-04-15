using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace RMUD3.Server.SignalR
{
	public interface IMiddlewareHubClient
	{
		Task Send(string[] componentPath, int action, object args);
	}

	public class MiddlewareHub : Hub<IMiddlewareHubClient>
	{
		public async Task Send(string[] componentPath, int action, JsonElement args, ISessionManagerService sessionManager)
		{
			Console.WriteLine($"Received client action: {action}. Args: {args}");
			await sessionManager.GetSession(Context.UserIdentifier).ComponentManager.HandleClientAction(componentPath, action, args);
		}

		public override Task OnConnectedAsync()
		{
			Console.WriteLine("Client connected!");
			return Task.CompletedTask;
		}
	}
}