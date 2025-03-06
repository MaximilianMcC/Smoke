public struct Project
{
	public static string Name { get; private set; }
	public static string DisplayName { get; private set; }
	public static string Path { get; private set; }
	public static string ProjectFilePath { get; private set; }

	public static void Load(string projectFilePath)
	{
		// Load the project file
		string[] projectFile = File.ReadAllLines(projectFilePath);
		ProjectFilePath = projectFilePath;

		// Get the name, display name, and filepath (lines 1, two, and 3ree)
		Name = projectFile[0].Trim();
		DisplayName = projectFile[1].Trim();
		Path = projectFile[2].Trim();
	}
}