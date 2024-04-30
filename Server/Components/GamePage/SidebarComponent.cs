using Tapper;

namespace RMUD3.Server.Components.GamePage
{
	[TranspilationSource]
	file class SidebarData
	{
		public string PlayerName { get; init; }
		public string LocationName { get; init; }
		public string[] Creatures { get; init; }

		public SidebarData(string playerName, string locationName, string[] creatures)
		{
			PlayerName = playerName;
			LocationName = locationName;
			Creatures = creatures;
		}
	}

	[TranspilationSource]
	file enum SidebarClientAction
	{
		RequestUpdate
	}

	[TranspilationSource]
	file enum SidebarServerAction
	{
		Update
	}

	public class SidebarComponent : Component
	{
		public SidebarComponent(GamePageComponent parent) : base("sidebar", parent.ComponentManager!, parent)
		{
			ActionHandler = new()
			{
				{ SidebarClientAction.RequestUpdate, ActionHandler.Wrap(() =>
					{
						var player = parent.Player;
						var location = player.Location ?? throw new Exception($"Player is not in a location! Location ID: {player.LocationId}");
						var sidebar = new SidebarData(player.Name, location.Name, location.Creatures.Where(c => c != player).Select(c => c.Name).ToArray());

						ExecuteAction(SidebarServerAction.Update, sidebar);
					})
				}
			};
		}
	}
}
