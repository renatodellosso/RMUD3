using System.Collections.Concurrent;

namespace RMUD3.Server
{
	public interface ISessionManagerService
	{
		void CreateSession(string? userId);
		public Session? GetSession(string? userId);
		public void SwapId(string? oldId, string? newId);
	}

	public class SessionManagerService : ISessionManagerService
	{

		private readonly ConcurrentDictionary<string, Session> sessions = new();

		public void CreateSession(string? userId)
		{
			ArgumentNullException.ThrowIfNull(userId);

			sessions.TryAdd(userId, new(new ClientCommunicationManager(userId), new ComponentManager()));
		}

		public Session? GetSession(string? userId)
		{
			ArgumentNullException.ThrowIfNull(userId);

			sessions.TryGetValue(userId, out Session? session);
			return session;
		}

		public void SwapId(string? oldId, string? newId)
		{
			ArgumentNullException.ThrowIfNull(oldId);
			ArgumentNullException.ThrowIfNull(newId);

			if (sessions.TryRemove(oldId, out Session? session))
			{
				session.ClientCommunicationHandler = new ClientCommunicationManager(newId);
				sessions.TryAdd(newId, session);
			}
		}
	}
}
