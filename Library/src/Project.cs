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

		// Custom json parsing stuff
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

		// Get the starting map
		// TODO: Do another way
		Map map = Info.Maps.FirstOrDefault(map => map.Name == Info.StartingMap);
		if (map != null) Info.CurrentMap = map;
	}

	//! do NOT modify the project settings in the game. Only engine
	public static void Save()
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

	public List<Prefab> Prefabs { get; set; }

	public List<Map> Maps { get; set; }
	public string StartingMap { get; set; }

	public Map CurrentMap { get; set; }
}