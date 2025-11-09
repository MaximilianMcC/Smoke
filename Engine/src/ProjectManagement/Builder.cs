class Builder
{
	// TODO: Make a way to also build smoke (for when working on the engine yk)
	public static void Build(string rootPath, bool debug)
	{
		// Find the csproj file
		string csprojPath = Directory.GetFiles(rootPath, "*.csproj").FirstOrDefault();
		if (csprojPath == null)
		{
			// Couldn't find a csproj file
			Console.WriteLine($"Could not find a project at '{rootPath}'. Make sure the path contains a .csproj file");
			return;
		}

		// If we are debugging then export a seprate dll, and then
		// if we are releasing then bake everything into a single exe
		if (debug) Debug(rootPath, csprojPath);
		else Release(rootPath, csprojPath);
	}

	private static void Debug(string rootPath, string csprojPath)
	{
		// Build the games code as a library so it becomes a dll
		// that we can inject into the runner program later on
		string dllOutputPath = Path.Join(rootPath, "bin", "assemblies");
		Utils.RunCliCommand($"dotnet build {csprojPath} --no-dependencies -o {dllOutputPath}");
	}

	private static void Release(string rootPath, string csprojPath)
	{
		// Build the games code as a library so it becomes a dll
		// that we can bake into the runner program later on
		string dllOutputPath = Path.Join(rootPath, "bin", "assemblies");
		Utils.RunCliCommand($"dotnet build {csprojPath} --no-dependencies --configuration Release -o {dllOutputPath}");
	}
}