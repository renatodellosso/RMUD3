using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RMUD3.Server.SignalR
{
	public class SessionIdBasedUserIdProvider(ISessionManagerService sessionManager) : IUserIdProvider
	{
		public string GetUserId(HubConnectionContext connection)
		{
			string? userId = connection.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId is null)
			{
				userId = Guid.NewGuid().ToString();
				connection.User.AddIdentity(new ClaimsIdentity(claims: [new(ClaimTypes.NameIdentifier, userId)]));
				sessionManager.CreateSession(userId);
			}

			return userId;
		}
	}
}
