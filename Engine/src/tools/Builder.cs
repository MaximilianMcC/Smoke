using System.Diagnostics;

class Builder
{
	// TODO: Don't hardcode
	public static string RunnerRootPath = @"D:\code\c#\raylib\Smoke\Runner\";
	public static string RunnerExePath = @"D:\code\c#\raylib\Smoke\Runner\bin\Release\net8.0\win-x64\publish\Runner.exe";
	public static string RunnerAssetsPath = Path.Combine(RunnerRootPath, "GameAssets");

	public static string Build(string csprojLocation, string outputPath) => Compile(csprojLocation, outputPath, false);
	public static string Publish(string csprojLocation, string outputPath) => Compile(csprojLocation, outputPath, true);

	public static string Compile(string csprojLocation, string outputPath, bool shouldPublish = false)
	{
		// Build settings idk
		string publish = shouldPublish ? "publish" : "build";
		string channel = shouldPublish ? "release" : "debug";

		// Make the actual build command
		ProcessStartInfo command = new ProcessStartInfo()
		{
			FileName = "dotnet",
			Arguments = $"{publish} \"{csprojLocation}\" -c {channel} -o \"{outputPath}\" --no-dependencies",

			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false
		};

		// Actually build it
		Process process = Process.Start(command);
		Console.WriteLine(process.StandardOutput.ReadToEnd());
		Console.WriteLine(process.StandardError.ReadToEnd());
		process.WaitForExit();

		// Delete the pdb and deps.json files
		foreach (string file in Directory.GetFiles(outputPath))
		{
			if (!(file.EndsWith(".pdb") || file.EndsWith(".deps.json"))) continue;
			File.Delete(file);
		}

		// Rename the output file
		// TODO: Make sure in the csproj <TargetName><TargetName> is Game
		const string TargetName = "Game.dll";
		string dllPath = Path.Combine(outputPath, TargetName);
		return dllPath;
	}

	public static void Package(string csprojLocation, string jsonPath, string outputPath)
	{
		// Delete everything from the previous build
		if (Directory.Exists(RunnerAssetsPath)) Directory.Delete(RunnerAssetsPath, true);

		// Build the games DLL
		Build(csprojLocation, RunnerAssetsPath);

		// Copy the games json file into the runners assets
		string newJsonPath = Path.Combine(RunnerAssetsPath, "Game.Json");
		File.Copy(jsonPath, newJsonPath);

		// Compile runner now that it has the required assets

		// Rename runner to whatever the game is called
		// and also move it to the requested output path


	}
}