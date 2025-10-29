using Newtonsoft.Json;

namespace Smoke;

internal class SmokeProject
{
	// Singleton
	// TODO: Maybe don't use a singleton ngl
	public static SmokeProject Instance { get; } = new();
	private SmokeProject() { }

	public ProjectSettings Settings = null;

	public void Load(string jsonFilePath)
	{
		// Deserialize the json
		string jsonString = File.ReadAllText(jsonFilePath);
		Settings = JsonConvert.DeserializeObject<ProjectSettings>(jsonString);

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