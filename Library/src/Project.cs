using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
		// TODO: Maybe don't use JObject but its the cleanest way to get static stuff
		Namespace = (string)projectJson["Namespace"];
		DisplayName = (string)projectJson["DisplayName"];
		RootPath = (string)projectJson["RootPath"];
		Version = Version.Parse((string)projectJson["Version"]);
		Restart = (int)projectJson["Restart"];

		// Cheeky debug
		Console.WriteLine($"Loaded '{DisplayName}' (v{Version}, r{Restart})\n{Namespace} → {RootPath}");

		// Get the fancy json settings so we can properly
		// pass the objects to the correct type/namespace
		JsonSerializerSettings settings = new JsonSerializerSettings()
		{
			TypeNameHandling = TypeNameHandling.Auto,
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
	}
}