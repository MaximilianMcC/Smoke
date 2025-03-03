using System.Reflection;

class ScriptManager
{
	private static FileSystemWatcher fileWatcher;
	public static Dictionary<string, IUpdatable> LoadedScripts;

	public static void Initialise(string scriptsPath)
	{
		// First manually load all scripts initially
		Directory.GetFiles(scriptsPath, "*.dll", SearchOption.AllDirectories)
			.ToList().ForEach(path => {
				
				// Make a dictionary thing to store the
				// path of the script
				LoadedScripts.Add(path, null);
				LoadScript(path);
			});

		// Set up a listener to listen for any
		// file changes in the scripts folder
		fileWatcher = new FileSystemWatcher(scriptsPath, "*.dll");
		fileWatcher.IncludeSubdirectories = true;

		// Load the script when the file changes
		fileWatcher.Changed += (s, e) => LoadScript(e.FullPath);
		fileWatcher.EnableRaisingEvents = true;
	}

	private static void LoadScript(string path)
	{
		// Double check the change isn't the file being removed
		//! Pretty sure this won't happen for the changed event
		if (File.Exists(path) == false) return;

		// Dynamically load the script
		byte[] scriptBytes = File.ReadAllBytes(path);
		Assembly script = Assembly.Load(scriptBytes);

		// Get the type of the script
		// TODO: Might not need this
		Type scriptType = script.GetTypes()
			.FirstOrDefault(type => typeof(IUpdatable)
			.IsAssignableFrom(type) && !type.IsInterface);
		
		// If we're loading an updatable script then
		// load it as an interface (updatable is an interface)
		// TODO: Use typeof
		if (scriptType is IUpdatable)
		{
			// Turn the assembly into an updatable
			IUpdatable loadedScript = (IUpdatable)Activator.CreateInstance(scriptType);
			LoadedScripts[path] = loadedScript;
		}
	}
}