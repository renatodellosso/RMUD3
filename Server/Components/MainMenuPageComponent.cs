using MongoDB.Bson;
using RMUD3.Server.Components.GamePage;
using RMUD3.Server.Content.Lists;
using RMUD3.Server.Exceptions;
using RMUD3.Server.Gameplay;
using Tapper;

namespace RMUD3.Server.Components
{
	// Can't transpile nested classes
	[TranspilationSource]
	file class MainMenuEnableData
	{
		public string Username { get; set; }
		public NewsItem[] News { get; set; }
		public PlayerSummary[] Players { get; set; }

		// Don't use a primary constructor! It will break the transpiler.
		public MainMenuEnableData(string username, NewsItem[] news, PlayerSummary[] players)
		{
			Username = username;
			News = news;
			Players = players;
		}
	}

	[TranspilationSource]
	file record PlayerSummary
	{
		public string Location { get; init; }
		public string Id { get; init; }
		public DateTime LastPlayed { get; init; }

		public PlayerSummary(string location, string id, DateTime lastPlayed)
		{
			Location = location;
			Id = id;
			LastPlayed = lastPlayed;
		}
	}

	[TranspilationSource]
	file enum MainMenuClientAction
	{
		NewGame,
		LoadGame
	}

	public class MainMenuPageComponent : Component
	{

		public MainMenuPageComponent(ComponentManager componentManager) : base("root", componentManager)
		{
			ActionHandler = new()
			{
				{ MainMenuClientAction.NewGame, ActionHandler.Wrap(() =>
					{
						var account = ComponentManager?.Session?.Account ?? throw new Exception("User not signed in, but tried to create new game!");

						Console.WriteLine($"Creating new player for account {account.Username}...");

						var player = new Player(account.Id, account.Username);
						account.Players.Add(player.Id);

						var db = Services.Db ?? throw new DbUnavailableException();

						db.Players.Add(player);
						db.Accounts.Update(account);
						db.SaveChanges();

						Console.WriteLine($"New player created for {player.Name}");

						StartGame(player);
					})
				},
				{ MainMenuClientAction.LoadGame, ActionHandler.Wrap((string rawId) =>
					{
						// Fetch player
						var id = ObjectId.Parse(rawId);
						var player = Services.Db?.Players.Find(id) ?? throw new Exception("Player not found in database!");

						// Verify player belongs to account
						var account = ComponentManager?.Session?.Account ?? throw new Exception("User not signed in, but tried to load game!");
						if (!account.Players.Contains(player.Id))
							throw new Exception("Player does not belong to account!");

						StartGame(player);
					})
				}
			};
		}

		protected override object? GetEnableData()
		{
			var account = ComponentManager?.Session?.Account;

			var players = account?.Players ?? [];
			var playerCollection = Services.Db?.Players ?? throw new DbUnavailableException();
			PlayerSummary[] summaries = new PlayerSummary[players.Count];
			for (int i = 0; i < players.Count; i++)
			{
				var player = playerCollection.Find(players[i]) ?? throw new Exception("Player not found in database!");
				summaries[i] = new PlayerSummary("N/A", player.Id.ToString(), player.LastPlayed);
			}
			summaries = [.. summaries.OrderByDescending(p => p.LastPlayed)];

			return new MainMenuEnableData(account?.Username ?? "Unknown", News.Items, summaries);
		}

		private void StartGame(Player player)
		{
			if (ComponentManager == null)
				throw new Exception("ComponentManager is null!");
			if (ComponentManager.Session == null)
				throw new Exception("ComponentManager.Session is null!");

			ComponentManager.Session.Player = player;
			player.Session = ComponentManager.Session;

			if (ComponentManager != null)
				ComponentManager.Root = new GamePageComponent(ComponentManager);
		}
	}
}
