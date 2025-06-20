using System.Reflection;
using Smoke;

class BuildToolsManager
{
	public static void AddVscode()
	{
		// Get the path to smoke
		string smokePath = AppManager.GetRunnerPath();
		if (smokePath == null) return;

		// Get where we are rn
		string root = Directory.GetCurrentDirectory();

		// Make the .vscode folder
		Directory.CreateDirectory(Path.Combine(root, ".vscode"));

		// Make the launch and tasks files (overwrites previous)
		File.WriteAllText(Path.Combine(root, "launch.json"), GenerateLaunchJson(smokePath));
		File.WriteAllText(Path.Combine(root, "tasks.json"), GenerateTasksJson(root));
	}

	private static string GenerateLaunchJson(string smokePath)
	{
		// Get the path of the debugging runner
		// TODO: Make debugging runner have hot-reload capabilities idk
		string runnerPath = Path.Combine(smokePath, "Runner.exe");

		// TODO: Remove the RootPath thingy from the json
		// Read the template file, and replace the needed bits
		return AssetManager.ReadTextFile("./assets/templates/launch.txt", Assembly.GetExecutingAssembly())
			.Replace("{runnerExe}", runnerPath);
	}

	private static string GenerateTasksJson(string root)
	{
		// Look for a csproj in the directory rn and
		// use that as the namespace thingy
		string namespaceName = Directory.GetFiles(root).Where(directory => directory.EndsWith(".csproj")).FirstOrDefault();
		if (namespaceName == default) Console.WriteLine("Can't find csproj file idk");

		// TODO: Remove the RootPath thingy from the json
		// Read the template file, and replace the needed bits
		return AssetManager.ReadTextFile("./assets/templates/tasks.txt", Assembly.GetExecutingAssembly())
			.Replace("{namespace}", namespaceName);
	}

	public static void AddBatch()
	{
		// Get the path to smoke
		string smokePath = AppManager.GetRunnerPath();
		if (smokePath == null) return;

		// Get where we are rn and put the batch file there
		string root = Directory.GetCurrentDirectory();
		File.WriteAllText(Path.Combine(root, "BuildAndRun.bat"), GenerateBatchFile(smokePath));
	}

	private static string GenerateBatchFile(string smokePath)
	{
		// Get the path of the debugging runner
		// TODO: Make debugging runner have hot-reload capabilities idk
		string runnerPath = Path.Combine(smokePath, "Runner.exe");

		return AssetManager.ReadTextFile("./assets/templates/tasks.txt", Assembly.GetExecutingAssembly())
			.Replace("{runnerPath}", runnerPath);
	}
} 