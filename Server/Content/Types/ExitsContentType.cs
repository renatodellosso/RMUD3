using RMUD3.Server.Gameplay;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RMUD3.Server.Content.Types
{
	public class ExitsContentType : ContentType
	{
		private class ExitContentFileContents
		{
			[JsonInclude]
			private string from, to;
			[JsonInclude]
			private ExitPos fromPos, toPos;
			[JsonInclude]
			private bool oneWay;

			public Exit GenerateExit() => new(from, fromPos, to, toPos, oneWay);

			public string From => from;
			public string To => to;
		}

		public override string TypeName => "Exits";

		public override Type[] Dependencies => [typeof(LocationContentType)];

		protected override void LoadFile(ContentFile file)
		{
			var exits = JsonSerializer.Deserialize<ExitContentFileContents[]>(file.Read()) ?? throw new ContentLoadException(file, "Failed to parse JSON");
			foreach (ExitContentFileContents exit in exits)
			{
				exit.GenerateExit().Init();
			}
		}
	}
}
