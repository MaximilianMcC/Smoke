class Runner
{
	public static void Debug(string rootPath, string runnerExePath)
	{
		// Get the project json
		string projectJsonPath = Directory.GetFiles(rootPath, "*.json").FirstOrDefault();

		// TODO: Maybe make the working directory the one of the project
		Utils.RunCliCommand($"{runnerExePath} {projectJsonPath}");
	}
}