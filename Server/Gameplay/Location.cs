namespace RMUD3.Server.Gameplay
{
	public class Location
	{
		public string? Id { get; set; }
		public string Name { get; set; }

		public Vector3 Size { get; init; }

		public string Description { get; init; }

		public HashSetWithEvents<Creature> Creatures { get; private init; }

		public Dictionary<ExitPos, Exit> Exits { get; private init; }

		public Location(string name, Vector3 size, string description)
		{
			Name = name;
			Size = size;

			Creatures = [];
			Creatures.OnAdd += OnCreatureEnter;
			Creatures.OnRemove += OnCreatureLeave;

			Exits = [];

			Description = description;
		}

		public void OnCreatureEnter(Creature creature)
		{
			creature.LocationId = Id;
		}

		public void OnCreatureLeave(Creature creature)
		{
		}

		protected void SendMsg(string msg)
		{
			foreach (var creature in Creatures)
			{
				if (creature is Player player)
				{
					if (player.Session == null)
						continue;

					var componentManager = player.Session?.ComponentManager;
				}
			}
		}
	}
}
