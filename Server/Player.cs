using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Tapper;

namespace RMUD3.Server
{
	[TranspilationSource]
	public class Player
	{
		[Key]
		[JsonIgnore]
		public ObjectId Id { get; private set; }

		[JsonIgnore]
		public ObjectId AccountId { get; private set; }

		public string Username { get; private set; }

		public DateTime LastPlayed { get; private set; }

		public Player(ObjectId accountId, string username)
		{
			Id = ObjectId.GenerateNewId();
			AccountId = accountId;
			Username = username;

			LastPlayed = DateTime.Now;
		}
	}
}
