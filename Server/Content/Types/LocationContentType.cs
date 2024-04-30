using RMUD3.Server.Content.Lists;
using RMUD3.Server.Gameplay;
using System.Text.Json;

namespace RMUD3.Server.Content.Types
{
	public class LocationContentType : ContentType
	{
		public override string TypeName => "Location";

		private readonly Dictionary<string, Location> locations;

		public LocationContentType()
		{
			locations = [];
		}

		protected override void LoadFile(ContentFile file)
		{
			var location = JsonSerializer.Deserialize<Location>(file.Read()) ?? throw new ContentLoadException(file, "Failed to parse JSON");
			location.Id = file.Name;

			try
			{
				locations.Add(file.Name, location);
			}
			catch (ArgumentException e)
			{
				throw new ContentLoadException(file, e);
			}
		}

		protected override void AfterLoad()
		{
			foreach (var location in locations.Values)
			{
				Locations.Add(location);
			}
		}
	}
}
