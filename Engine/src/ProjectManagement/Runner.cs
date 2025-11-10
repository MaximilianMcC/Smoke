class Runner
{
	public static void Debug(string rootPath, string runnerExePath)
	{
		// TODO: Maybe make the working directory the one of the project
		Utils.RunCliCommand($"{runnerExePath} {rootPath}");
	}
}