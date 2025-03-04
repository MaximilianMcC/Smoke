using System.Diagnostics;

class Builder
{
	public static void Build()
	{
		// Find all scripts in the project
		string scriptsPath = Path.Combine(Project.Path, "src");
		string[] scriptPaths = Directory.GetFiles(scriptsPath, "*.cs", SearchOption.AllDirectories);
		foreach (string scriptPath in scriptPaths)
		{
			Compile(scriptPath);
		}
	}

	public static void Compile(string scriptPath)
	{	
		// Get all the libraries
		string libraryArguments = "";
		{
			// Store all the libraries we're gonna be using
			List<string> libraries = new List<string>();

			// Get the path to the library
			// TODO: Hardcode, but not like this. Should be okish for debugging, but maybe put as arg or something
			string libraryPath = @"D:\code\c#\raylib\MarlEngine\Library\bin\debug\net8.0\Library.dll";
			libraries.Add(libraryPath);

			// Get the paths to the runtime libraries
			string[] runtimeLibraries = GetCoreLibraryPaths();
			libraries.AddRange(runtimeLibraries);

			// Convert the library paths into arguments
			libraries.ForEach(library => libraryArguments += $"-r:\"{library}\" ");
		}

		// Get the output path for the scripts dll
		string outputFileName = Path.GetFileNameWithoutExtension(scriptPath) + ".dll";
		string outputPathArgument = Path.Combine(Project.Path, "compiled", outputFileName);

		// Make sure the output path is real
		Directory.CreateDirectory(Path.GetDirectoryName(outputPathArgument));

		// Make the compile command
		ProcessStartInfo command = new ProcessStartInfo()
		{
			FileName = "csc",
			Arguments = $"-target:library -out:\"{outputPathArgument}\" {libraryArguments} \"{scriptPath}\"",

			CreateNoWindow = true,
			RedirectStandardOutput = true,
			RedirectStandardError = true
		};

		Console.WriteLine(command.FileName + " " + command.Arguments);

		// Run the command to compile everything
		Process process = new Process();
		process.StartInfo = command;
		process.Start();

		// Nick the error stream
		string processErrorOutput = process.StandardOutput.ReadToEnd();
		process.WaitForExit();

		// Check for if there were any errors
		if (string.IsNullOrEmpty(processErrorOutput) == false)
		{
			// Throw a tanty and pack a sad
			Console.WriteLine($"Error while compiling {Path.GetFileName(scriptPath)}\n{processErrorOutput}");
			return;
		}

		// csc -target:library -out:bin\Test.dll -r:"D:\code\c#\raylib\MarlEngine\Library\bin\Debug\net8.0\Library.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Private.CoreLib.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Runtime.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Console.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Linq.dll" .\src\Test.cs
	}

	private static string[] GetCoreLibraryPaths()
	{
		// Get the path to the dotnet runtime dlls
		//? runtime dlls are the bare minimum needed to run/compile a C# program
		string coreLibraryPath = System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory();

		// Get all libraries needed
		//* Maybe remove console and write my own (still using streams)
		// TODO: Maybe just hardcode this ngl since its only these three (will not be changing)
		string[] librariesNeeded = [ "Private.CoreLib", "Runtime", "Console", "Linq" ];
		string[] libraryPaths = new string[librariesNeeded.Length];
		for (int i = 0; i < librariesNeeded.Length; i++)
		{
			// Construct the full path to the library
			libraryPaths[i] = Path.Join(coreLibraryPath, $"System.{librariesNeeded[i]}.dll");
		}

		// Give back the library paths
		return libraryPaths;
	}
}