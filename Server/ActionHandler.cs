using System.Text.Json;

namespace RMUD3.Server
{
	public class ActionHandler : Dictionary<int, Func<JsonElement?, Task>>
	{
		public static Func<JsonElement?, Task> Wrap(Action action)
		{
			return async (json) =>
			{
				action();
				await Task.CompletedTask;
			};
		}

		public static Func<JsonElement?, Task> Wrap<TArgs>(Action<TArgs> action)
		{
			return async (json) =>
			{
				if (json == null)
					throw new ArgumentNullException(nameof(json));

				TArgs typedArgs = JsonSerializer.Deserialize<TArgs>(json!.Value.GetRawText()) ?? throw new Exception("Could not parse args as json.");
				action(typedArgs);
				await Task.CompletedTask;
			};
		}

		public static Func<JsonElement?, Task> Wrap<TArgs>(Func<TArgs, Task> action)
		{
			return async (json) =>
			{
				if (json == null)
					throw new ArgumentNullException(nameof(json));

				TArgs typedArgs = JsonSerializer.Deserialize<TArgs>(json!.Value.GetRawText()) ?? throw new Exception("Could not parse args as json.");
				await action(typedArgs);
			};
		}

		public void Add<TKey, TArgs>(TKey key, Action action) where TKey : Enum => base.Add(key.GetHashCode(), Wrap(action));
		public void Add<TKey, TArgs>(TKey key, Action<TArgs> action) where TKey : Enum => base.Add(key.GetHashCode(), Wrap(action));
		public void Add<TKey, TArgs>(TKey key, Func<TArgs, Task> action) where TKey : Enum => base.Add(key.GetHashCode(), Wrap(action));
		/// <summary>
		/// Collection initialization will call <see cref="Add{TKey, TArgs}(TKey, Func{TArgs, Task})"/> 
		/// if we don't have this, so we need to add this overload to avoid wrapping the method twice.
		/// </summary>
		public void Add<TKey>(TKey key, Func<JsonElement?, Task> action) where TKey : Enum => base.Add(key.GetHashCode(), action);

		public Tuple<Enum, Func<JsonElement?, Task>> this[Enum key]
		{
			get => new(key, base[key.GetHashCode()]);
			set => base[key.GetHashCode()] = value.Item2;
		}
	}
}
