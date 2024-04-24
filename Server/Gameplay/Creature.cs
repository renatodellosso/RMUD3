using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
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
			get; set;
		}

		protected Creature()
		{
			Id = ObjectId.GenerateNewId();
		}
	}
}
