namespace RMUD3.Server.Gameplay
{
	public class Location
	{
		public string? Id { get; set; }
		public string Name { get; set; }

		public Vector3 Size { get; init; }

		public HashSetWithEvents<Creature> Creatures { get; private init; }

		public Dictionary<ExitPos, Exit> Exits { get; private init; }

		public Location(string name, Vector3 size)
		{
			Name = name;
			Size = size;

			Creatures = [];
			Creatures.OnAdd += OnCreatureEnter;
			Creatures.OnRemove += OnCreatureLeave;

			Exits = [];
		}

		public void OnCreatureEnter(Creature creature)
		{
			creature.LocationId = Id;
		}

		public void OnCreatureLeave(Creature creature)
		{
		}
	}
}
