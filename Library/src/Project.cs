using System.Text.Json;

public class Project
{
	public static ProjectInfo Info;
	public static string RootPath;
	public static string ProjectJsonPath;
	private static JsonSerializerOptions options;

	public static void Load(string projectFilePath)
	{
		// Set the project file path
		RootPath = Path.GetDirectoryName(Path.GetFullPath(projectFilePath));
		ProjectJsonPath = projectFilePath;

		// Custom parsing stuff
		options = new JsonSerializerOptions()
		{
			Converters = {
				new ComponentConverter(),
				new Vector2Converter(),
				new Vector3Converter()
			}
		};

		// Parse the whole project JSON file
		string projectJson = File.ReadAllText(ProjectJsonPath);
		Info = JsonSerializer.Deserialize<ProjectInfo>(projectJson, options);

		// say its loaded and stuff
		Console.WriteLine("Loaded project " + Info.Name);
	}

	//! do NOT modify the project settings in the game. Only engine
	public static void Save(bool withIndentation = false)
	{
		// Serialize the json as camelCase
		string projectJson = JsonSerializer.Serialize(Info, options);

		// Rewrite the project json
		File.WriteAllText(ProjectJsonPath, projectJson);
	}
}

public class ProjectInfo
{
	// TODO: Rename Name to Namespace
	public string Name { get; set; }
	public string DisplayName { get; set; }
	public string ProjectPath { get; set; }

	public List<GameObject> GameObjects { get; set; }
}