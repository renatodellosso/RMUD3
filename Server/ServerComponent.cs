using System.Text.Json;

namespace RMUD3.Server
{
	public class ServerComponent(string id)
	{

		public string Id { get; init; } = id;

		private readonly Dictionary<int, Func<JsonElement, Task>> actionHandlers = [];

		private readonly Dictionary<string, ServerComponent> children = [];

		protected void AddActionHandler<TKey, TArgs>(TKey key, Func<TArgs, Task> action) where TKey : Enum
		{
			actionHandlers.Add(key.GetHashCode(), async (json) =>
			{
				TArgs typedArgs = JsonSerializer.Deserialize<TArgs>(json.GetRawText()) ?? throw new Exception("Could not parse args as json.");
				await action(typedArgs);
			});
		}

		protected void AddActionHandler<TKey, TArgs>(TKey key, Action<TArgs> action) where TKey : Enum
		{
			AddActionHandler(key, (TArgs args) =>
			{
				action(args);
				return Task.CompletedTask;
			});
		}

		public Task HandleClientAction(Queue<string> componentPath, int actionId, JsonElement args)
		{
			if (componentPath.Count == 0)
			{
				if (actionHandlers.TryGetValue(actionId, out Func<JsonElement, Task>? action))
					return action(args);
				else
					throw new Exception($"No action handler found for action id {actionId}.");
			}
			else
			{
				string nextComponentId = componentPath.Dequeue();
				if (children.TryGetValue(nextComponentId, out ServerComponent? next))
					return next.HandleClientAction(componentPath, actionId, args);
				else
					throw new Exception($"No component found with id {nextComponentId}.");
			}
		}
	}
}
