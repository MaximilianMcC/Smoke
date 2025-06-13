using System.Diagnostics;

class Builder
{
	// TODO: Don't hardcode
	public static string RunnerExePath = @"D:\code\c#\raylib\Smoke\Runner\bin\Release\net8.0\win-x64\publish\Runner.exe";

	public static string Build(string csprojLocation)
	{
		// Build the actual game DLL
		ProcessStartInfo buildDllCommand = new ProcessStartInfo()
		{
			FileName = "dotnet",
			Arguments = $"build {csprojLocation} -c Release --no-dependencies",

			CreateNoWindow = true,
			RedirectStandardOutput = true,
			RedirectStandardError = true
		};

		// Run the command idk
		Process command = new Process();
		command.StartInfo = buildDllCommand;
		command.Start();

		// Rename the DLL to game.dll
		string outputPath = Path.Combine(csprojLocation, "bin", "Release");
		File.Move(outputPath, Path.Join(Path.GetDirectoryName(outputPath), "game.dll"));

		// Return the path of the DLL
		return Path.Combine(outputPath, $"game.dll");
	}

	public static void Publish(string csprojLocation, string jsonPath, string outputPath)
	{
		// Build the games DLL
		string dllPath = Build(csprojLocation);

		// Copy the JSON and DLL into the runner
		// project so they can become embedded
		string runnerEmbeddedAssetsPath = Path.Join(Path.GetDirectoryName(RunnerExePath), "GameAssets");
		File.Copy(dllPath, runnerEmbeddedAssetsPath);
		File.Copy(jsonPath, runnerEmbeddedAssetsPath);
	}
}