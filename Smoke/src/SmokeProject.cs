using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Smoke;

internal class SmokeProject
{
	public static SmokeConfiguration Config = null;
	internal static string ConfigJson;
	private static string jsonPath;

	// TODO: Maybe do this in another way also idk if this really needs to be public
	public static JsonSerializer JsonDeserializerSettings = JsonSerializer.Create(new JsonSerializerSettings()
	{
		// Stuff for assemblies
		TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
		SerializationBinder = new DefaultSerializationBinder(),

		// Defines the datatype in a "$type" property
		TypeNameHandling = TypeNameHandling.Auto,
	});

	public static void Load(string rootPath)
	{
		// Get the project json
		jsonPath = Path.Combine(rootPath, "Project.json");

		// Deserialize the json
		ConfigJson = File.ReadAllText(jsonPath);
		Config = JsonConvert.DeserializeObject<SmokeConfiguration>(ConfigJson);

		// Load the dll to import all the actual game code
		// TODO: Support hot reloading
		string dllPath = Path.Combine(rootPath, "bin", "assemblies", $"{Config.Namespace}.dll");
		Assembly.LoadFrom(dllPath);

		// Cheeky debug message
		Console.WriteLine($"Loaded project of namespace '{Config.Namespace}'");
	}

	public static void Save()
	{
		// Combine Config and all scenes into a single json thing
    	JObject json = JObject.FromObject(Config, JsonDeserializerSettings);
    	json["Scenes"] = JArray.FromObject(SceneManager.Scenes, JsonDeserializerSettings);

		// Write it
		File.WriteAllText(jsonPath, json.ToString());
	}

	public static void CreateDefault(string rootPath, string projectName)
	{
		// Start putting stuff in
		Config = new SmokeConfiguration()
		{
			Namespace = projectName,
			DisplayName = projectName
		};

		// Save the json to a file
		Save();
	}

	// The json file
	public class SmokeConfiguration
	{
		public string Namespace { get; set; }
		public string DisplayName { get; set; }

		public string StartingScene { get; set; }
	}
}