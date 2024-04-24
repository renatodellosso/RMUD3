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

		public string Username { get; private set; }

		public DateTime LastPlayed { get; private set; }

		public Player(ObjectId accountId, string username) : base()
		{
			AccountId = accountId;
			Username = username;

			LastPlayed = DateTime.Now;
		}
	}
}
