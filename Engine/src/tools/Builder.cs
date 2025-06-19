using System.Diagnostics;

class Builder
{
	// TODO: Don't hardcode
	public static readonly string RunnerRootPath = @"D:\code\c#\raylib\Smoke\Runner\";
	public static readonly string RunnerCsprojPath = @"D:\code\c#\raylib\Smoke\Runner\Runner.csproj";
	public static readonly string RunnerExePath = @"D:\code\c#\raylib\Smoke\Runner\bin\Release\net8.0\win-x64\publish\Runner.exe";
	public static readonly string RunnerAssetsPath = Path.Combine(RunnerRootPath, "GameAssets");

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
		GetRidOfCrap(outputPath);

		// Rename the output file
		// TODO: Make sure in the csproj <TargetName><TargetName> is Game
		const string TargetName = "Game.dll";
		string dllPath = Path.Combine(outputPath, TargetName);
		return dllPath;
	}

	public static void Package(string gameName, string csprojLocation, string jsonPath, bool publish, string outputPath)
	{
		// Delete everything from the previous build
		if (Directory.Exists(RunnerAssetsPath)) Directory.Delete(RunnerAssetsPath, true);

		// Build the games DLL
		Build(csprojLocation, RunnerAssetsPath);

		// Copy the games json file into the runners assets
		string newJsonPath = Path.Combine(RunnerAssetsPath, "Game.Json");
		File.Copy(jsonPath, newJsonPath);

		// Compile runner now that it has the required assets
		CompileRunner(publish, outputPath);
		GetRidOfCrap(outputPath);

		// Rename runner to whatever the game is called
		// and also move it to the requested output path
		string exePath = Path.Combine(outputPath, "Runner.exe");
		string newExePath = Path.Combine(outputPath, gameName + ".exe");
		File.Move(exePath, newExePath);
	}

	public static void CompileRunner(bool shouldPublish, string outputPath)
	{
		// Build settings idk
		string publish = shouldPublish ? "publish" : "build";
		string channel = shouldPublish ? "Release" : "Debug";

		string specialPublishSettings = "";
		if (shouldPublish) specialPublishSettings = "/p:IncludeNativeLibrariesForSelfExtract=true";

		// Make the actual build command
		// TODO: Maybe add RID (win-x64 etc)
		ProcessStartInfo command = new ProcessStartInfo()
		{
			FileName = "dotnet",
			Arguments = $"{publish} \"{RunnerCsprojPath}\" -c {channel} --self-contained true /p:PublishSingleFile=true --no-dependencies {specialPublishSettings} -o \"{outputPath}\"",

			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false
		};

		// Actually build it
		Process process = Process.Start(command);
		Console.WriteLine(process.StandardOutput.ReadToEnd());
		Console.WriteLine(process.StandardError.ReadToEnd());
		process.WaitForExit();
	}

	// Get rid of pdb and deps.json files
	private static void GetRidOfCrap(string directoryPath)
	{
		foreach (string file in Directory.GetFiles(directoryPath))
		{
			if (!(file.EndsWith(".pdb") || file.EndsWith(".deps.json"))) continue;
			File.Delete(file);
		}
	}
}