using Newtonsoft.Json.Linq;

namespace Smoke;

internal class SmokeProject
{
	// Singleton
	public static SmokeProject Instance { get; } = new();
	private SmokeProject() { }

	// TODO: Use a class and don't do the dynamic key thing
	public string Namespace;
	public string DisplayName;

	public void Load(string jsonFilePath)
	{
		// Deserialize the json
		string jsonString = File.ReadAllText(jsonFilePath);
		JObject json = JObject.Parse(jsonString);

		// Start extracting stuff
		Namespace = (string)json["Namespace"];
		DisplayName = (string)json["Namespace"];

		// Cheeky debug message
		Console.WriteLine($"Loaded project of namespace '{Namespace}'");
	}

	public void CreateDefault(string rootPath, string projectName)
	{
		// Start putting stuff in
		JObject newProjectFile = new JObject();
		newProjectFile["Namespace"] = projectName;
		newProjectFile["Displayname"] = projectName;

		// Save the json to a file
		string jsonString = newProjectFile.ToString();
		File.WriteAllText(Path.Join(rootPath, "Project.json"), jsonString);
	}
}