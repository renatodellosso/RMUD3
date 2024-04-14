using Microsoft.AspNetCore.SignalR;
using RMUD3.Server.ServerComponents;
using RMUD3.Server.SignalR;
using System.Text.Json;

namespace RMUD3.Server
{
	public class Session(string userId)
	{

		private readonly string userId = userId;

		private IHubContext<MiddlewareHub, IMiddlewareHubClient> hubContext = Services.GetHubContext();

		private IMiddlewareHubClient Client => hubContext.Clients.User(userId);

		private ServerComponent page = new MainPageServerComponent();

		public async Task HandleClientAction(string[] componentPath, int actionId, JsonElement args)
		{
			await page.HandleClientAction(new Queue<string>(componentPath), actionId, args);
		}

	}
}
