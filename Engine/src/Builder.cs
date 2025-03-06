using System.Diagnostics;

class Builder
{
	public static void Build()
	{
		Compile();
	}

	public static void Compile()
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

		Console.WriteLine(command.FileName + " " + command.Arguments);

		// Run the command to compile everything
		Process process = new Process();
		process.StartInfo = command;
		process.Start();

		// Wait for it to run
		// TODO: Do this in another thread
		process.WaitForExit();
	}
}