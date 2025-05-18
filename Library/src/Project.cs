using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Smoke;

public class Project
{
	public static string Namespace;
	public static string DisplayName;
	public static string RootPath;
	public static string JsonPath;
	public static Version Version;
	public static int Restart;

	public static void Load(string projectJsonFilePath)
	{
		// Nick the json path quickly
		JsonPath = projectJsonFilePath;

		// Load the JSON
		string json = File.ReadAllText(JsonPath);
		JObject projectJson = JObject.Parse(json);

		// Pick out keys individually
		Namespace = (string)projectJson["Namespace"];
		DisplayName = (string)projectJson["DisplayName"];
		RootPath = (string)projectJson["RootPath"];
		Version = Version.Parse((string)projectJson["Version"]);
		Restart = (int)projectJson["Restart"];

		// Dynamically inject the assembly from the game
		// (all the actual code written for the engine)
		// TODO: Support loading of multiple DLLs
		string assemblyPath = Path.Join(RootPath, "bin", "assemblies", $"{Namespace}.dll");
		Assembly assembly = Assembly.LoadFrom(assemblyPath);

		// Loop through all Types in the imported 
		// assembly and create an instance of them 
		// so the game objects can be deserialized
		// TODO: Don't do this (seems dodge)
		foreach (Type type in assembly.GetTypes())
		{
			assembly.CreateInstance(type.Name);
		}

		// Get the fancy json settings so we can properly
		// pass the objects to the correct type/namespace
		JsonSerializerSettings settings = new JsonSerializerSettings()
		{
			// Stuff for assemblies
			TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
			SerializationBinder = new DefaultSerializationBinder(),

			// Defines the datatype in a "$type" property
			TypeNameHandling = TypeNameHandling.Auto,

			// Tabs
			Formatting = Formatting.Indented
		};
		JsonSerializer deserializer = JsonSerializer.Create(settings);

		// Parse &→ load all the game objects
		JArray rawGameObjects = (JArray)projectJson["GameObjects"];
		GameObjectManager.GameObjects = rawGameObjects.ToObject<List<GameObject>>(deserializer);

		// Loop through all components and reassign
		// their parent game objects because they
		// are lost during serialization to json
		foreach (GameObject gameObject in GameObjectManager.GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.GameObject = gameObject;
			}
		}

		// Cheeky debug message
		Console.WriteLine($"Loaded '{DisplayName}' (v{Version}, r{Restart})\n{Namespace} → {RootPath}");
	}
}