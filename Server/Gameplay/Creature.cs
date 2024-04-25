using MongoDB.Bson;
using RMUD3.Server.Content.Lists;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tapper;

namespace RMUD3.Server.Gameplay
{
	[TranspilationSource]
	public class Creature
	{
		[Key]
		[JsonIgnore]
		public ObjectId Id { get; private set; }

		public virtual string Name
		{
			get; protected set;
		}

		[JsonIgnore]
		public string? LocationId { get; set; }

		[JsonIgnore]
		[NotMapped]
		public Location? Location
		{
			get => Locations.Get(LocationId ?? throw new Exception($"Location not set for creature: {Name}"));
			set => LocationId = value?.Id;
		}

		protected Creature()
		{
			Id = ObjectId.GenerateNewId();
			Name = "Unnamed " + GetType().Name;
		}
	}
}
