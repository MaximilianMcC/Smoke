using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Smoke;

class ProjectMaker
{
	private static string projectName;
	private static string rootPath;

	public static void CreateNewProject(string rawName)
	{
		// Make sure the name is in the correct format,
		// and that there is not already a project with
		// the same name in the location
		projectName = ToPascalCase(rawName);
		rootPath = Path.Combine(Directory.GetCurrentDirectory(), projectName);
		if (Directory.Exists(rootPath))
		{
			// TODO: If its an empty git project then ignore
			Console.WriteLine($"A project with the name '{projectName}' already exists!");
			return;
		}

		Console.Write("Creating project...");
		MakeDotnetProject();
		EditCsproj();
		AddGitIgnore();
		SetupFolderStructure();
		CreateJsonFile();
		Console.WriteLine($"\rDone! Created project '{projectName}' at {rootPath}\nOpen it in editor with 'smoke {projectName}'");
	}

	private static void MakeDotnetProject()
	{
		Utils.RunSilentCliCommand($"dotnet new classlib -o \"{rootPath}\"");
	}

	private static void EditCsproj()
	{
		// load the csproj file as an xml thing
		string csprojPath = Path.Combine(rootPath, $"{projectName}.csproj");
		XDocument csproj = XDocument.Load(csprojPath);

		// Set all the property stuff
		XElement propertyGroup = csproj.Root.Element("PropertyGroup");
		propertyGroup.SetElementValue("RootNamespace", projectName);
		propertyGroup.SetElementValue("OutputType", "library");
		propertyGroup.SetElementValue("Nullable", "disable");
		propertyGroup.SetElementValue("ImplicitUsings", "disable");
		propertyGroup.SetElementValue("NoWarn", "0649");

		// Add the smoke library
		//TODO: Make the path dyanimc (hardcoded for now)
		XElement smokeReference = new XElement("ItemGroup",
			new XElement("ProjectReference", new XAttribute("Include", @"D:\code\c#\raylib\Smoke\Smoke\Smoke.csproj"))
		);
		csproj.Root.Add(smokeReference);

		// Add the assets folder
		XElement assetsFolder = new XElement("ItemGroup",
			new XElement("EmbeddedResource", new XAttribute("Include", @"assets\**\*.*"))
		);
		csproj.Root.Add(assetsFolder);

		// Save the csproj edits
		csproj.Save(csprojPath);
	}

	// TODO: Add vscode folder or something
	private static void AddGitIgnore()
	{
		Utils.RunSilentCliCommand($"dotnet new gitignore", rootPath);
	}

	private static void SetupFolderStructure()
	{
		// First delete the default file it makes
		File.Delete(Path.Join(rootPath, "Class1.cs"));

		// Make a source and assets directory
		Directory.CreateDirectory(Path.Join(rootPath, "src"));
		Directory.CreateDirectory(Path.Join(rootPath, "assets"));
	}

	private static void CreateJsonFile()
	{
		SmokeProject.Instance.CreateDefault(rootPath, projectName);
	}

	// Project names must be PascalCase
	private static string ToPascalCase(string text)
	{
		// Split the words by '-', '_', and ' ' (snake and kebab case + a normal space)
		string[] words = Regex.Split(text, "[-_ ]");

		string pascalCase = "";
		foreach (string word in words)
		{
			// First letter a capital, everything else lower
			pascalCase += char.ToUpper(word[0]) + word[1..].ToLower();
		}

		return pascalCase;
	}
}