using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace RMUD3.server
{
	public interface ISessionManagerService
	{
		void CreateSession(ClaimsPrincipal user);
	}

	public class SessionManagerService : ISessionManagerService
	{

		private readonly ConcurrentDictionary<string, Session> sessions = new();

		private readonly IHubContext<MiddlewareHub, IMiddlewareHubClient> hubContext;

		public SessionManagerService(IHubContext<MiddlewareHub, IMiddlewareHubClient> hubContext)
		{
			Console.WriteLine("SessionManagerService created");

			this.hubContext = hubContext;
		}

		public void CreateSession(ClaimsPrincipal user)
		{
			Session session = new(user);

			string guid = Guid.NewGuid().ToString();
			user.AddIdentity(new ClaimsIdentity(claims: [new(ClaimTypes.NameIdentifier, guid)]));

			Console.WriteLine($"Creating session with guid {user.GetSessionId()}");

			sessions.TryAdd(guid, session);
		}
	}
}
