using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace RMUD3.Server.SignalR
{
	public interface IMiddlewareHubClient
	{
		Task Action(string[] componentPath, int action, object? args = null);

		Task EnableComponent(string[] componentPath, string componentType, object? componentData = null);
		Task DisableComponent(string[] componentPath);

		Task SetSessionId(string? sessionId);
	}

	public class MiddlewareHub(ISessionManagerService sessionManager) : Hub<IMiddlewareHubClient>
	{
		public async Task Action(string[] componentPath, int action, JsonElement? args = null)
		{
			Session? session = sessionManager.GetSession(Context.UserIdentifier);
			if (session != null && session.ComponentManager != null)
				await session.ComponentManager.HandleClientAction(componentPath, action, args);
		}

		public async Task ClientConnect(string sessionId)
		{
			if (sessionId != null && Context.User != null)
			{
				Session? session = sessionManager.GetSession(sessionId);

				if (session != null)
				{
					Console.WriteLine($"Client reconnected!");

					var newId = Context.UserIdentifier;
					sessionManager.SwapId(sessionId, newId);

					session.ComponentManager?.Root?.Enable();
					await Clients.Caller.SetSessionId(Context.UserIdentifier);

					return;
				}
			}

			// Either the session ID was not provided or the session does not exist
			Console.WriteLine($"Client connected!");
			sessionManager.CreateSession(Context.UserIdentifier);
			await Clients.Caller.SetSessionId(Context.UserIdentifier);
		}

		public override Task OnConnectedAsync()
		{
			//Console.WriteLine("Client connected!");

			return Task.CompletedTask;
		}
	}
}