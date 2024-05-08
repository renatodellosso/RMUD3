using System.Text.Json;
using System.Text.Json.Serialization;

namespace RMUD3.Server.Gameplay
{
	public enum Side
	{
		North,
		East,
		South,
		West,
		Up,
		Down
	}

	[JsonConverter(typeof(ExitPosJsonConverter))]
	public record ExitPos(Side Side, Vector2 Pos);

	public class ExitPosJsonConverter : JsonConverter<ExitPos>
	{
		public override ExitPos? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var content = reader.GetString() ?? throw new JsonException();

			var side = content[0] switch
			{
				'N' => Side.North,
				'E' => Side.East,
				'S' => Side.South,
				'W' => Side.West,
				'U' => Side.Up,
				'D' => Side.Down,
				_ => throw new JsonException("Invalid Side. Side: " + content[0])
			};

			// We have to add back " " to the content to make it a valid json string
			var posStr = content[1..];
			var pos = "UD".Contains(content[0]) ? JsonSerializer.Deserialize<Vector2>($"\"{content[1..]}\"") : new(int.Parse(posStr), 0);

			return new ExitPos(side, pos);
		}

		public override void Write(Utf8JsonWriter writer, ExitPos value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}

}
