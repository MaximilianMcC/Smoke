using System.Reflection;
using Newtonsoft.Json;

namespace Smoke;

internal class SmokeProject
{
	// Singleton
	// TODO: Maybe don't use a singleton ngl
	public static SmokeProject Instance { get; } = new();
	private SmokeProject() { }

	public ProjectSettings Settings = null;

	public void Load(string rootPath)
	{
		// Deserialize the json
		string jsonString = File.ReadAllText(Path.Combine(rootPath, "Project.json"));
		Settings = JsonConvert.DeserializeObject<ProjectSettings>(jsonString);

		// Load the dll to import all the actual game code
		// TODO: Support hot reloading
		string dllPath = Path.Combine(rootPath, "bin", "assemblies", $"{Settings.Namespace}.dll");
		Assembly.LoadFrom(dllPath);

		// Cheeky debug message
		Console.WriteLine($"Loaded project of namespace '{Settings.Namespace}'");
	}

	public void CreateDefault(string rootPath, string projectName)
	{
		// Start putting stuff in
		ProjectSettings settings = new ProjectSettings()
		{
			Namespace = projectName,
			DisplayName = projectName
		};

		// Save the json to a file
		string jsonString = JsonConvert.SerializeObject(settings);
		File.WriteAllText(Path.Join(rootPath, "Project.json"), jsonString);
	}

	// The json file
	public class ProjectSettings
	{
		public string Namespace { get; set; }
		public string DisplayName { get; set; }
	}
}