class Program
{
	// TODO: Make a config file where this is specificed
	//! hardcoded for now
	//! ITS HARDCODED OK
	public static string RunnerExePath = @"D:\code\c#\raylib\Smoke\Runner\bin\Debug\net8.0\Runner.exe";

	public static void Main(string[] args)
	{
		// Ensure we have supplied args
		if (args.Length == 0)
		{
			Console.WriteLine("No arguments supplied. Please use one of the following");
			Console.WriteLine("smoke new <PROJECT NAME>");
			Console.WriteLine("smoke <PROJECT NAME>");
			Console.WriteLine("smoke build <PROJECT ROOT> <debug|release>");
			Console.WriteLine("smoke run <PROJECT ROOT>");
			return;
		}

		// Check for if we are opening the editor or making a new project
		if (args[0].ToLower().Trim() == "new")
		{
			// Get the new projects name
			if (args.Length < 1)
			{
				Console.WriteLine("Please supply a project name. For example:");
				Console.WriteLine("smoke new \"MyAwesomeGame\"");
				return;
			}
			string projectName = args[1].Trim();

			// Make the new project
			ProjectMaker.CreateNewProject(projectName);
		}
		else if (args[0].ToLower().Trim() == "build")
		{
			// Get the new projects root path
			string projectRoot = Directory.GetCurrentDirectory();
			if (args.Length > 0) projectRoot = args[1].Trim();

			if (Utils.IsDirectoryASmokeProject(projectRoot) == false) return;

			// Get the new projects build type
			string buildType = "release";
			if (args.Length >= 2) buildType = args[2].Trim();
			if (buildType != "debug" && buildType != "release")
			{
				// TODO: Don't do here
				Console.WriteLine($"please either use debug or release (not {buildType})");
				return;
			}

			// Build it
			Builder.Build(projectRoot, buildType == "debug");
		}
		else if (args[0].ToLower().Trim() == "run")
		{
			// Get the new projects root path
			string projectRoot = Directory.GetCurrentDirectory();
			if (args.Length > 0) projectRoot = args[1].Trim();
			if (Utils.IsDirectoryASmokeProject(projectRoot) == false) return;

			Runner.Debug(projectRoot, RunnerExePath);
		}
		else
		{
			// Opening an existing project
			// Engine.Run();
		}
	}
}