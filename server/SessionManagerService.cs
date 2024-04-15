using System.Collections.Concurrent;

namespace RMUD3.Server
{
	public interface ISessionManagerService
	{
		void CreateSession(string userId);
		public Session GetSession(string? userId);
	}

	public class SessionManagerService : ISessionManagerService
	{

		private readonly ConcurrentDictionary<string, Session> sessions = new();

		public void CreateSession(string userId)
		{
			sessions.TryAdd(userId, new(new ClientCommunicationManager(userId), new ComponentManager()));
		}

		public Session GetSession(string? userId)
		{
			if (userId is null)
				throw new ArgumentNullException("ctx.UserIdentifier");

			if (!sessions.TryGetValue(userId, out Session? session))
				throw new Exception($"Session not found for user {userId}");

			return session;
		}
	}
}
