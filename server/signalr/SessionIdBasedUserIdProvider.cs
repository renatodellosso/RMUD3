using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace RMUD3.Server.SignalR
{
	public class SessionIdBasedUserIdProvider : IUserIdProvider
	{
		public string GetUserId(HubConnectionContext connection)
		{
			string? userId = connection.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			if (userId is null)
			{
				SetUserId(connection.User, userId = Guid.NewGuid().ToString());
			}

			return userId;
		}

		public static void SetUserId(ClaimsPrincipal user, string userId)
		{
			user.AddIdentity(new ClaimsIdentity(claims: [new(ClaimTypes.NameIdentifier, userId)]));
		}
	}
}
