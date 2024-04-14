using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace RMUD3.server
{
	public interface ISessionManagerService
	{
		void CreateSession(string userId);
		public Session GetSession(HubCallerContext ctx);

	}

	public class SessionManagerService : ISessionManagerService
	{

		private readonly ConcurrentDictionary<string, Session> sessions = new();

		public void CreateSession(string userId)
		{
			sessions.TryAdd(userId, new(userId));
		}

		public Session GetSession(HubCallerContext ctx)
		{
			if (ctx.UserIdentifier is null)
				throw new ArgumentNullException("ctx.UserIdentifier");

			if (!sessions.TryGetValue(ctx.UserIdentifier, out Session? session))
				throw new Exception($"Session not found for user {ctx.UserIdentifier}");

			return session;
		}
	}
}
