using MongoDB.Bson;
using System.Text.Json.Serialization;
using Tapper;

namespace RMUD3.Server.Gameplay
{
	[TranspilationSource]
	public class Player : Creature
	{

		[JsonIgnore]
		public ObjectId AccountId { get; private set; }

		public DateTime LastPlayed { get; private set; }

		[JsonIgnore]
		public Session? Session { get; set; }

		public Player(ObjectId accountId, string name) : base()
		{
			AccountId = accountId;
			Name = name;

			LastPlayed = DateTime.Now;

			LocationId = "Start";
		}
	}
}
