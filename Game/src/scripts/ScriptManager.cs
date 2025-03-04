using System.Reflection;

class ScriptManager
{
	private static FileSystemWatcher fileWatcher;
	public static List<IUpdatable> LoadedScripts;

	public static void Initialise(string scriptsPath)
	{
		// Store all the loaded scripts
		LoadedScripts = new List<IUpdatable>();

		// First manually load all scripts initially
		Directory.GetFiles(scriptsPath, "*.dll", SearchOption.AllDirectories)
			.ToList().ForEach(path => LoadScript(path));

		/*
		// Set up a listener to listen for any
		// file changes in the scripts folder
		fileWatcher = new FileSystemWatcher(scriptsPath, "*.dll");
		fileWatcher.IncludeSubdirectories = true;

		// Load the script when the file changes
		fileWatcher.Changed += (s, e) => LoadScript(e.FullPath);
		fileWatcher.EnableRaisingEvents = true;
		*/
	}

	private static void LoadScript(string path)
	{
		// Double check the change isn't the file being removed
		//! Pretty sure this won't happen for the changed event
		if (File.Exists(path) == false) return;

		// Dynamically load the script
		byte[] scriptBytes = File.ReadAllBytes(path);
		Assembly script = Assembly.Load(scriptBytes);

		// Get the type of class in the script
		Type scriptType = script.GetTypes()
			.FirstOrDefault(type => typeof(IUpdatable).IsAssignableFrom(type));

		// Turn the assembly into an updatable
		if (scriptType != null)
		{
			IUpdatable loadedScript = (IUpdatable)Activator.CreateInstance(scriptType);
			LoadedScripts.Add(loadedScript);
		}
	}
}