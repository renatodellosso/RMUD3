using RMUD3.Server.Content.Lists;

namespace RMUD3.Server.Gameplay
{
	public class Exit
	{
		public Location From { get; private init; }
		public Location To { get; private init; }

		private bool oneWay;

		public Exit(string from, string to, bool oneWay)
		{
			From = Locations.Get(from);
			To = Locations.Get(to);
			this.oneWay = oneWay;
		}

		public void Enter(Creature creature)
		{
			if (creature.LocationId == From.Id)
			{
				From.Creatures.Remove(creature);
				To.Creatures.Add(creature);
			}
			else if (!oneWay && creature.LocationId == To.Id)
			{
				To.Creatures.Remove(creature);
				From.Creatures.Add(creature);
			}
			else
			{
				throw new System.Exception(
					$"Creature is not in the correct location to enter exit ({From.Id} => {To.Id}{(oneWay ? " (one way)" : "")}). Current Location: {creature.LocationId}");
			}
		}
	}
}
