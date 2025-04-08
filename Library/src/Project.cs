using System.Text.Json;

public class Project
{
	public static ProjectInfo Info;
	public static string RootPath;
	private static string projectJsonPath;

	public static void Load(string projectFilePath)
	{
		// Set the project file path
		RootPath = Path.GetDirectoryName(Path.GetFullPath(projectFilePath));
		projectJsonPath = projectFilePath;

		// Custom parsing stuff
		JsonSerializerOptions options = new JsonSerializerOptions()
		{
			Converters = {
				new ComponentConverter()
			}
		};

		// Parse the whole project JSON file
		string projectJson = File.ReadAllText(projectJsonPath);
		Info = JsonSerializer.Deserialize<ProjectInfo>(projectJson, options);

		// say its loaded and stuff
		Console.WriteLine("Loaded project " + Info.Name);
	}

	//! do NOT modify the project settings in the game. Only engine
	public static void Save(bool withIndentation = false)
	{
		// Serialize the json as camelCase
		string projectJson = JsonSerializer.Serialize(Info, new JsonSerializerOptions {

			PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			WriteIndented = withIndentation,
		});

		// Rewrite the project json
		File.WriteAllText(projectJsonPath, projectJson);
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