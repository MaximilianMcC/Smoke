using System.Diagnostics;

class Utils
{
	public static void RunCliCommand(string command, string workingDirectory = null)
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
}