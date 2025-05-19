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
		Assembly.LoadFrom(assemblyPath);

		// Parse &→ load all the game objects
		JArray rawGameObjects = (JArray)projectJson["Prefabs"];
		ObjectManager.DeserializeObjects(rawGameObjects);

		// Cheeky debug message
		Console.WriteLine($"Loaded '{DisplayName}' (v{Version}, r{Restart})\n{Namespace} → {RootPath}");
	}
}