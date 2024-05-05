using MongoDB.Bson;
using RMUD3.Server.Components.GamePage;
using System.ComponentModel.DataAnnotations.Schema;
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

		[JsonIgnore, NotMapped]
		public Session? Session { get; set; }

		public Player(ObjectId accountId, string name) : base()
		{
			AccountId = accountId;
			Name = name;

			LastPlayed = DateTime.Now;

			LocationId = "Start";
		}

		public void SendMsg(string msg)
		{

			var componentManager = Session?.ComponentManager;
			if (componentManager == null) return;
			if (componentManager.Root == null) return;

			var log = componentManager.Root.GetComponent<LogComponent>();
			log.SendMsg(msg);
		}
	}
}
