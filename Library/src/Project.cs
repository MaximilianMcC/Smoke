using System.Text.Json;

public class Project
{
	public static ProjectInfo ProjectInfo;
	public static string ProjectFilePath;

	public static void Load(string projectFilePath)
	{
		// Set the project file path
		ProjectFilePath = projectFilePath;

		// Parse the whole project JSON file
		string projectJson = File.ReadAllText(projectFilePath);
		ProjectInfo = JsonSerializer.Deserialize<ProjectInfo>(projectJson);
	}

	//! do NOT modify the project settings in the game. Only engine
	public static void Save(bool withIndentation = false)
	{
		// Serialize the json as camelCase
		string projectJson = JsonSerializer.Serialize(ProjectInfo, new JsonSerializerOptions {

			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = withIndentation,
		});

		// Rewrite the project json
		File.WriteAllText(ProjectFilePath, projectJson);
	}
}

public class ProjectInfo
{
	public string Name { get; set; }
	public string DisplayName { get; set; }
	public string ProjectPath { get; set; }

	public List<GameObject> gameObjects { get; set; }
}