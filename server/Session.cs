using Microsoft.AspNetCore.SignalR;
using RMUD3.Server.clientactiondicts;
using RMUD3.Server.SignalR;
using System.Text.Json;

namespace RMUD3.Server
{
	public class Session(string userId)
	{

		private readonly string userId = userId;

		private IHubContext<MiddlewareHub, IMiddlewareHubClient> hubContext = Services.GetHubContext();

		private IMiddlewareHubClient Client => hubContext.Clients.User(userId);

		private ClientActionDict actionDict = new MainPageClientActionDict();

		public async Task HandleClientAction(int actionId, JsonElement args)
		{
			actionDict.TryGetValue(actionId, out var action);

			if (action is null)
				throw new Exception($"No action found for id: {actionId}.");

			action(args);

			await Client.Send(actionId, args);
		}

	}
}
