public struct Project
{
	public static string Name { get; private set; }
	public static string Path { get; private set; }
	public static string ProjectFilePath { get; private set; }

	public static void Load(string projectFilePath)
	{
		// Load the project file
		string[] projectFile = File.ReadAllLines(projectFilePath);
		ProjectFilePath = projectFilePath;

		// Get the name and filepath (lines 1 and 2)
		Name = projectFile[0].Trim();
		Path = projectFile[1].Trim();

		Console.WriteLine($"Loaded project \"{Name}\"");
	}
}