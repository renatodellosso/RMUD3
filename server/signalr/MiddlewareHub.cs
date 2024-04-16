using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace RMUD3.Server.SignalR
{
	public interface IMiddlewareHubClient
	{
		Task Action(string[] componentPath, int action, object? args = null);

		Task EnableComponent(string[] componentPath, string componentType, object? componentData = null);
		Task DisableComponent(string[] componentPath);
	}

	public class MiddlewareHub(ISessionManagerService sessionManager) : Hub<IMiddlewareHubClient>
	{
		public async Task Action(string[] componentPath, int action, JsonElement? args = null)
		{
			Session session = sessionManager.GetSession(Context.UserIdentifier);
			if (session != null && session.ComponentManager != null)
				await session.ComponentManager.HandleClientAction(componentPath, action, args);
		}

		public override Task OnConnectedAsync()
		{
			Console.WriteLine("Client connected!");
			sessionManager.CreateSession(Context.UserIdentifier);
			return Task.CompletedTask;
		}
	}
}