using System.Text.Json;

namespace RMUD3.Server
{

	public class ComponentManager
	{

		private Session? session;
		public Session? Session
		{
			get => session;
			set
			{
				if (session is not null)
					throw new InvalidOperationException("Session already set");
				session = value;
			}
		}

		private Component? root;

		/// <summary>
		/// Automatically disables the old root and enables the new root, to ensure the client always has a page loaded.
		/// </summary>
		public Component? Root
		{
			get => root;
			set
			{
				root?.Disable();
				root = value;
				root?.Enable();
			}
		}

		public Task HandleClientAction(string[] componentPath, int action, JsonElement? args)
		{
			if (root is null)
				throw new InvalidOperationException("Root component not set");

			return root.HandleClientAction(new Queue<string>(componentPath), action, args);
		}
	}
}