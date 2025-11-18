using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Smoke;

internal class SmokeProject
{
	public static SmokeConfiguration Config = null;
	internal static string ConfigJson;

	public static void Load(string rootPath)
	{
		// Deserialize the json
		ConfigJson = File.ReadAllText(Path.Combine(rootPath, "Project.json"));
		Config = JsonConvert.DeserializeObject<SmokeConfiguration>(ConfigJson);

		// Load the dll to import all the actual game code
		// TODO: Support hot reloading
		string dllPath = Path.Combine(rootPath, "bin", "assemblies", $"{Config.Namespace}.dll");
		Assembly.LoadFrom(dllPath);

		// Cheeky debug message
		Console.WriteLine($"Loaded project of namespace '{Config.Namespace}'");
	}

	public static void CreateDefault(string rootPath, string projectName)
	{
		// Start putting stuff in
		SmokeConfiguration settings = new SmokeConfiguration()
		{
			Namespace = projectName,
			DisplayName = projectName
		};

		// Save the json to a file
		string jsonString = JsonConvert.SerializeObject(settings);
		File.WriteAllText(Path.Join(rootPath, "Project.json"), jsonString);
	}

	// The json file
	public class SmokeConfiguration
	{
		public string Namespace { get; set; }
		public string DisplayName { get; set; }

		public string CurrentScene { get; set; }
	}
}