using System.Diagnostics;

class Builder
{
	public static string GamePath;
	public static bool CurrentlyBuilding = false;

	public static void BuildAndRun()
	{
		Build();
		Run();
	}

	public static void Run()
	{
		// Run the game as a child of engine
		Process game = Process.Start(GamePath, Project.ProjectFilePath);
		game.EnableRaisingEvents = true;

		// If we close the engine then also close the game
		AppDomain.CurrentDomain.ProcessExit += (s, e) => game.Kill();
	}

	public static void Build()
	{
		// Start to compile the code in a new thread
		CurrentlyBuilding = true;
		Thread compileThread = new Thread(Compile);
		compileThread.Start();
	}

	private static void Compile()
	{
		// Get the assembly output path
		// and the csproj path
		string outputPath = Path.Combine(Project.Path, "bin", "assemblies");
		string csprojPath = Path.Combine(Project.Path, Project.Name + ".csproj");

		// TODO: Maybe like delete the assemblies folder every like 10 runs to clean it

		// Make the compile command
		ProcessStartInfo command = new ProcessStartInfo()
		{
			// TODO: Make so you can toggle between debug and not
			FileName = "dotnet",
			Arguments = $"build \"{csprojPath}\" --no-dependencies -o \"{outputPath}\"",

			CreateNoWindow = true,
			RedirectStandardOutput = true,
			RedirectStandardError = true
		};

		// Run the command to compile everything
		Process process = new Process();
		process.StartInfo = command;
		process.Start();

		// Wait for it to run
		process.WaitForExit();
		CurrentlyBuilding = false;
	}
}