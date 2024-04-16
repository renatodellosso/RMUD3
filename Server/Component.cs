using System.Text.Json;

namespace RMUD3.Server
{
	public class Component
	{

		public string Id { get; }

		private bool enabled;

		private readonly Session session;

		public ActionHandler? ActionHandler { get; set; }

		private readonly Component? parent;
		public string[] Path => parent == null ? [] : parent.Path.Append(Id).ToArray();

		private readonly Dictionary<string, Component> children = [];

		public Component(string id, Session session, Component? parent = null)
		{
			Id = id;
			this.session = session;
			this.parent = parent;
		}

		public Task HandleClientAction(Queue<string> componentPath, int actionId, JsonElement? args)
		{
			if (componentPath.Count == 0)
			{
				if (ActionHandler == null)
					throw new Exception("No action handler set.");

				if (ActionHandler.TryGetValue(actionId, out Func<JsonElement?, Task>? action))
					return action(args);
				else
					throw new Exception($"No action handler found for action id {actionId}.");
			}
			else
			{
				string nextComponentId = componentPath.Dequeue();
				if (children.TryGetValue(nextComponentId, out Component? next))
					return next.HandleClientAction(componentPath, actionId, args);
				else
					throw new Exception($"No component found with id {nextComponentId}.");
			}
		}

		/// <summary>
		/// Enable the component. Notifies the client that it should create the corresponding client component.
		/// </summary>
		public void Enable()
		{
			session?.ClientCommunicationHandler?.Client?.EnableComponent(Path, GetType().Name, GetEnableData());
			enabled = true;
			EnableChildren();
		}

		/// <summary>
		/// Calls <see cref="Enable"/> on all children.
		/// </summary>
		private void EnableChildren()
		{
			foreach (Component child in children.Values)
				child.Enable();
		}

		protected virtual object? GetEnableData() => null;

		/// <summary>
		/// Disable the component. Notifies the client that it should remove the corresponding client component.
		/// </summary>
		public void Disable()
		{
			session?.ClientCommunicationHandler?.Client?.DisableComponent(Path);
			enabled = false;
			DisableChildren();
		}

		/// <summary>
		/// Calls <see cref="Disable"/> on all children.
		///	</summary>
		private void DisableChildren()
		{
			foreach (Component child in children.Values)
				child.Disable();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="actionId"></param>
		/// <param name="args"></param>
		/// <exception cref="Exception"></exception>
		protected void ExecuteAction(Enum action, object? args = null)
		{
			if (enabled)
				session?.ClientCommunicationHandler?.Client?.Action(Path, action.GetHashCode(), args);
			else
				throw new Exception("Component is disabled.");
		}
	}
}
