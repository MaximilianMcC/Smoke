using System.Diagnostics;
using System.Xml.Linq;

class ProjectMaker
{
	private static string projectName;
	private static string rootPath;

	public static void CreateNewProject(string rawName)
	{
		// Make sure the name is in the correct format,
		// and that there is not already a project with
		// the same name in the location
		projectName = CleanName(rawName);
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
		CreateFiles();
		Console.WriteLine($"\rDone! Created project '{projectName}' at {rootPath}\nOpen it in editor with 'smoke {projectName}'");
	}

	private static void MakeDotnetProject()
	{
		// Create the command
		ProcessStartInfo command = new ProcessStartInfo
		{
			FileName = "dotnet",
			Arguments = $"new classlib -o \"{rootPath}\"",

			RedirectStandardOutput = true,
			RedirectStandardError = true,

			UseShellExecute = false,
			CreateNoWindow = true
		};

		// Run the command and wait for it to
		// finish before continuing
		Process.Start(command).WaitForExit();
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

	private static void AddGitIgnore()
	{
		// Create the command
		ProcessStartInfo command = new ProcessStartInfo
		{
			FileName = "dotnet",
			Arguments = $"new gitignore",
			WorkingDirectory = rootPath,

			RedirectStandardOutput = true,
			RedirectStandardError = true,

			UseShellExecute = false,
			CreateNoWindow = true
		};

		// Run the command and wait for it to
		// finish before continuing
		Process.Start(command).WaitForExit();
	}

	private static void CreateFiles()
	{
		// First delete the default file it makes
		File.Delete(Path.Join(rootPath, "Class1.cs"));

		// Make a source and assets directory
		Directory.CreateDirectory(Path.Join(rootPath, "src"));
		Directory.CreateDirectory(Path.Join(rootPath, "assets"));
	}

	// Project names must be PascalCase
	// TODO: Fix
	private static string CleanName(string name)
	{
		//? Snake and Kebab respectivly
		char[] caseSeparators = new char[] { '_', '-' };
		List<char> newName = name.ToCharArray().ToList();

		if (name == "") return "SmokeProject";

		// First word must be capital
		newName[0] = char.ToUpper(name[0]);

		// Get rid of any weird case things
		for (int i = 0; i < newName.Count; i++)
		{
			// Check for if we found a separator
			if (caseSeparators.Contains(name[i]))
			{
				// Remove the separator
				newName.RemoveAt(i);

				// If there is a character infront of
				// the seprator then make it uppercase
				if (i < newName.Count)
				{
					newName[i] = char.ToUpper(newName[i]);
				}

				// Go back since we removed one
				// before (indexs messed up)
				i--;
			}
		}

		// Give back the new version
		return new string(newName.ToArray());
	}
}