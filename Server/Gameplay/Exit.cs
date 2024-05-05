using RMUD3.Server.Content.Lists;

namespace RMUD3.Server.Gameplay
{
	public class Exit
	{
		public Location From { get; private init; }

		public Location To { get; private init; }

		public bool OneWay { get; private init; }

		public Exit(string from, ExitPos fromPos, string to, ExitPos toPos, bool oneWay)
		{
			From = Locations.Get(from);
			From.Exits.Add(fromPos, this);
			To = Locations.Get(to);
			To.Exits.Add(toPos, this);
			OneWay = oneWay;
		}

		public void Enter(Creature creature)
		{
			if (creature.LocationId == From.Id)
			{
				From.Creatures.Remove(creature);
				To.Creatures.Add(creature);
			}
			else if (!OneWay && creature.LocationId == To.Id)
			{
				To.Creatures.Remove(creature);
				From.Creatures.Add(creature);
			}
			else
			{
				throw new Exception(
					$"Creature is not in the correct location to enter exit ({From.Id} => {To.Id}{(OneWay ? " (one way)" : "")}). Current Location: {creature.LocationId}");
			}
		}

		public Location GetOtherLocation(Location location)
		{
			if (location == From)
				return To;
			else if (location == To)
				return From;
			else
				throw new Exception("Location is not part of this exit.");
		}
	}
}
