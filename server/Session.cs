using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RMUD3.server
{
	public class Session
	{

		private ClaimsPrincipal claims;

		private IHubContext<MiddlewareHub, IMiddlewareHubClient> hubContext;

		private IMiddlewareHubClient Client => hubContext.Clients.User;

		public Session(ClaimsPrincipal claims)
		{
			this.claims = claims;

			hubContext = Services.GetHubContext();
		}

	}
}
