using Tapper;

namespace RMUD3.Server.Components
{
	public class MainMenuPageComponent : Component
	{
		public MainMenuPageComponent(ComponentManager componentManager) : base("root", componentManager)
		{
		}

		[TranspilationSource]
		class MainMenuEnableData
		{
			public string Username { get; set; }
			public NewsItem[] News { get; set; }

			// Don't use a primary constructor! It will break the transpiler.
			public MainMenuEnableData(string username, NewsItem[] news)
			{
				Username = username;
				News = news;
			}
		}

		protected override object? GetEnableData()
		{
			return new MainMenuEnableData(ComponentManager?.Session?.Account?.Username ?? "Unknown", [new("Test", "4/21/24", "Hello"), new("Test2", "4/21/1912", "Owen smells")]);
		}
	}
}
