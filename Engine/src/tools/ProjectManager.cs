using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Smoke;

class ProjectManager
{
	public static string templateCsprojPath = "";
	public static string templateJsonPath = "";
	public static string smokeImport = "<ProjectReference Include=\"../Smoke/Library/Smoke.csproj\"/>";

	public static void CreateNewProject(string name)
	{

		// Clean up the name
		string ogName = name;
		name = ToPascalCase(name);

		// Make the C# project
		string projectDirectory = MakeCSharpProject(name);
		Console.WriteLine(projectDirectory);

		// Remove the default class file it gives us
		File.Delete(Path.Join(projectDirectory, "Class1.cs"));

		// Make a src and assets directory
		Directory.CreateDirectory(Path.Join(projectDirectory, "src"));
		Directory.CreateDirectory(Path.Join(projectDirectory, "assets"));

		// Make the game.json file
		// TODO: Put the default thingy somewhere idk
		File.WriteAllText(Path.Combine(projectDirectory, "Game.json"), GenerateJson(name, ogName), Encoding.UTF8);

		// Remake the csproj file
		File.Delete(Path.Combine(projectDirectory, name + ".csproj"));
		File.WriteAllText(Path.Combine(projectDirectory, name + ".csproj"), GenerateCsproj(name));
	}

	private static string GenerateCsproj(string name)
	{
		AssetManager.PrintAllAssets(Assembly.GetExecutingAssembly());

		// Read the template file, and replace the needed bits
		return AssetManager.ReadTextFile("./assets/templates/csproj.txt", Assembly.GetExecutingAssembly())
			.Replace("{name}", name)
			.Replace("{smokeImport}", smokeImport);
	}

	private static string GenerateJson(string namespaceName, string displayName)
	{
		// TODO: Remove the RootPath thingy from the json
		// Read the template file, and replace the needed bits
		return AssetManager.ReadTextFile("./assets/templates/json.txt", Assembly.GetExecutingAssembly())
			.Replace("{namespace}", namespaceName)
			.Replace("{name}", displayName);
	}

	private static string MakeCSharpProject(string name)
	{
		// Make the new C# library project
		ProcessStartInfo command = new ProcessStartInfo()
		{
			FileName = "dotnet",
			Arguments = $"new classlib -o {name}",

			RedirectStandardOutput = true,
			RedirectStandardError = true,
			UseShellExecute = false
		};

		// Run the command
		Process process = Process.Start(command);
		process.WaitForExit();

		// Give back the path of the project
		return Path.Combine(Directory.GetCurrentDirectory(), name);
	}

	private static string ToPascalCase(string text)
	{
		// Split the words by '-', '_' and ' ' (snake and kebab case + a normal space)
		string[] words = Regex.Split(text, "[-_ ]");

		string pascalCase = "";
		foreach (string word in words)
		{
			// First letter a capital, everything else lower
			pascalCase += Char.ToUpper(word[0]) + word[1..].ToLower();
		}

		return pascalCase;
	}
}