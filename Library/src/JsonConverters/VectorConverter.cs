
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

class Vector2Converter : JsonConverter<Vector2>
{
	public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Get the json object we're gonna be parsing
		using JsonDocument json = JsonDocument.ParseValue(ref reader);
		JsonElement root = json.RootElement;

		// Extract the X and Y
		float x = root.GetProperty("X").GetSingle();
		float y = root.GetProperty("Y").GetSingle();
		return new Vector2(x, y);
	}

	public override void Write(Utf8JsonWriter writer, Vector2 vector, JsonSerializerOptions options)
	{
		// Write in the X and Y
		writer.WriteStartObject();
		writer.WriteNumber("X", vector.X);
		writer.WriteNumber("Y", vector.Y);
		writer.WriteEndObject();
	}
}

class Vector3Converter : JsonConverter<Vector3>
{
	public override Vector3 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Get the json object we're gonna be parsing
		using JsonDocument json = JsonDocument.ParseValue(ref reader);
		JsonElement root = json.RootElement;

		// Extract the X and Y
		float x = root.GetProperty("X").GetSingle();
		float y = root.GetProperty("Y").GetSingle();
		float z = root.GetProperty("Z").GetSingle();
		return new Vector3(x, y, z);
	}

	public override void Write(Utf8JsonWriter writer, Vector3 vector, JsonSerializerOptions options)
	{
		// Write in the X and Y
		writer.WriteStartObject();
		writer.WriteNumber("X", vector.X);
		writer.WriteNumber("Y", vector.Y);
		writer.WriteNumber("Z", vector.Z);
		writer.WriteEndObject();
	}
}