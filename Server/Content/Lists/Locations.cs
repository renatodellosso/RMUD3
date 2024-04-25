﻿using RMUD3.Server.Gameplay;

namespace RMUD3.Server.Content.Lists
{
	public class Locations
	{
		private static Locations? instance;

		private Dictionary<string, Location> locations;

		private Locations()
		{
			locations = [];
		}

		public static Location Get(string name)
		{
			instance ??= new Locations();

			if (instance.locations.TryGetValue(name, out Location? value))
				return value;

			throw new KeyNotFoundException();
		}

		public static void Add(Location location)
		{
			instance ??= new Locations();

			instance.locations.Add(location.Id ?? throw new ArgumentException($"Location Id not set for location: {location.Name}"), location);
		}
	}
}
