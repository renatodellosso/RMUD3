using System.Collections.Immutable;

namespace RMUD3.Server.Gameplay
{
	public class Location
	{
		public string? Id { get; set; }
		public string Name { get; set; }

		public Vector3 Size { get; init; }

		public string Description { get; init; }

		private readonly HashSet<Creature> creatures;
		public ImmutableArray<Creature> Creatures => [.. creatures];

		public Dictionary<ExitPos, Exit> Exits { get; private init; }

		public Location(string name, Vector3 size, string description)
		{
			Name = name;
			Size = size;

			creatures = [];

			Exits = [];

			Description = description;
		}

		public void Enter(Creature creature, Exit? exit)
		{
			creatures.Add(creature);
			creature.LocationId = Id;

			SendMsg($"{creature.Name} enters the room.");

			if (creature is Player player)
				player.SendMsg(Description);
		}

		public void Exit(Creature creature, Exit? exit)
		{
			creatures.Remove(creature);
			SendMsg($"{creature.Name} exits the room.");
		}

		protected void SendMsg(string msg)
		{
			foreach (var creature in creatures)
			{
				if (creature is Player player)
					player.SendMsg(msg);
			}
		}
	}
}
