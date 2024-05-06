using RMUD3.Server.Gameplay;
using System.Text.Json;

namespace RMUD3.Server.Content.Types
{
	public class ExitsContentType : ContentType
	{
		private class ExitContentFileContents
		{
			private string from, to;
			private ExitPos fromPos, toPos;
			private bool oneWay;

			public Exit generateExit() =>
		}

		public override string TypeName => "Exits";

		protected override void LoadFile(ContentFile file)
		{
			var exits = JsonSerializer.Deserialize<ExitContentFileContents[]>(file.Read()) ?? throw new ContentLoadException(file, "Failed to parse JSON");
		}
	}
}
