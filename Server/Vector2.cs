using System.Text.Json;
using System.Text.Json.Serialization;
using Tapper;

namespace RMUD3.Server
{
	[TranspilationSource]
	[JsonConverter(typeof(Vector2JsonConverter))]
	public struct Vector2
	{
		public float X { get; set; }
		public float Y { get; set; }

		public Vector2(float x, float y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"<{X}, {Y}>";
		}
	}

	public class Vector2JsonConverter : JsonConverter<Vector2>
	{
		public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var content = reader.GetString() ?? throw new JsonException();
			var parts = content.Split(",");

			if (parts.Length == 2)
				return new Vector2(float.Parse(parts[0]), float.Parse(parts[1]));

			throw new JsonException("Incorrect length");
		}

		public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
