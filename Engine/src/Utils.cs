using System.Diagnostics;

class Utils
{
	// Run a cli command without showing output
	public static void RunSilentCliCommand(string command, string workingDirectory = null)
	{
		// Create the command
		ProcessStartInfo process = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/C " + command,
			WorkingDirectory = workingDirectory,

			RedirectStandardOutput = true,
			RedirectStandardError = true,

			UseShellExecute = false,
			CreateNoWindow = true
		};

		// Run the command and wait for it to
		// finish before continuing
		Process.Start(process).WaitForExit();
	}

	// Run a cli command and print the output
	public static void RunCliCommand(string command, string workingDirectory = null)
	{
		// Create the command
		ProcessStartInfo process = new ProcessStartInfo
		{
			FileName = "cmd.exe",
			Arguments = "/C " + command,
			WorkingDirectory = workingDirectory,

			RedirectStandardOutput = false,
			RedirectStandardError = false,

			UseShellExecute = false,
			CreateNoWindow = false
		};

		// Run the command and wait for it to
		// finish before continuing
		Process.Start(process).WaitForExit();
	}

	// TODO: Make it kill the program
	public static bool IsDirectoryASmokeProject(string path)
	{
		// Must both have a .csproj and a .json file
		bool HasCsproj = Directory.GetFiles(path, "*.csproj").FirstOrDefault() != null;
		bool HasJson = Directory.GetFiles(path, "Project.json").FirstOrDefault() != null;

		// Complain if we're missing the things
		if ((HasCsproj && HasJson) == false)
		{
			Console.WriteLine($"Invalid smoke prokect at \"{path}\"! Ensure you have both a Project json and csproj file.\nYou can create a smoke project with `smoke new <NAME>`");

			// Is not valid
			return false;
		}

		// Is valid
		return true;
	}
}