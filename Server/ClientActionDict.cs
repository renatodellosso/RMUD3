using System.Text.Json;

namespace RMUD3.Server
{
	public class ClientActionDict : Dictionary<int, Action<JsonElement>>
	{
		protected void Add<TKey, TArgs>(TKey key, Action<TArgs> action) where TKey : Enum
		{
			Add(key.GetHashCode(), (json) =>
			{
				TArgs typedArgs = JsonSerializer.Deserialize<TArgs>(json.GetRawText()) ?? throw new Exception("Could not parse args as json.");
				action(typedArgs);
			});
		}
	}
}
