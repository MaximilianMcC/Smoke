
using System.Text.Json;
using System.Text.Json.Serialization;

class ComponentConverter : JsonConverter<IComponent>
{
	public override IComponent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		// Get the json object we're gonna be parsing
		using JsonDocument json = JsonDocument.ParseValue(ref reader);
		JsonElement root = json.RootElement;

		// All components
		// TODO: make this const static or whatever yk
		Dictionary<string, Type> components = new Dictionary<string, Type>()
		{
			{ "Script", typeof(ScriptComponent) },
			{ "Transform", typeof(Transform) }
		};

		// Read the type of component, then deserialize the
		// correct class based on the type string
		string type = root.GetProperty("Type").GetString();
		if (components.TryGetValue(type, out Type component))
		{
			// Parse the raw text into the required type
			return (IComponent)JsonSerializer.Deserialize(root.GetRawText(), component, options);
		}

		// Wrong type or something
		throw new NotSupportedException($"idk what {type} is. Double check the spelling or something idk");
	}

	public override void Write(Utf8JsonWriter writer, IComponent value, JsonSerializerOptions options)
	{
		// erhm idk
		JsonSerializer.Serialize(writer, (object)value, value.GetType(), options);
	}
}