using System.Text.Json;
using System.Text.Json.Serialization;
using Tapper;

namespace RMUD3.Server
{
	[TranspilationSource]
	[JsonConverter(typeof(Vector3JsonConverter))]
	public struct Vector3
	{
		/// <summary>
		/// East-West
		/// </summary>
		public float X { get; set; }
		/// <summary>
		/// North-Souh
		/// </summary>
		public float Y { get; set; }
		/// <summary>
		/// Down-Up
		/// </summary>
		public float Z { get; set; }

		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public override string ToString()
		{
			return $"<{X}, {Y}, {Z}>";
		}
	}

	public class Vector3JsonConverter : JsonConverter<Vector3>
	{
		public override Vector3 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var content = reader.GetString() ?? throw new JsonException();
			var parts = content.Split(",");

			if (parts.Length == 3)
				return new Vector3(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]));

			throw new JsonException();
		}

		public override void Write(Utf8JsonWriter writer, Vector3 value, JsonSerializerOptions options)
		{
			throw new NotImplementedException();
		}
	}
}
