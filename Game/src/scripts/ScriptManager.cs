using System.Reflection;
using System.Runtime.Loader;

class ScriptManager
{
	private static FileSystemWatcher fileWatcher;
	private static List<AssemblyLoadContext> loadedAssemblies = [];

	public static List<IUpdatable> LoadedLogicScripts = [];
	public static List<IRenderable> LoadedRenderableScripts = [];

	public static void Initialise()
	{
		// Get the path to all the assemblies
		string assembliesPath = Path.Join(Project.RootPath, "bin", "assemblies");

		// First manually load all scripts initially
		Directory.GetFiles(assembliesPath, "*.dll", SearchOption.AllDirectories)
			.ToList().ForEach(path => LoadAssembly(path));

		// Set up a listener to listen for any
		// file changes in the scripts folder
		fileWatcher = new FileSystemWatcher(assembliesPath, "*.dll");
		fileWatcher.IncludeSubdirectories = true;

		// Load the script when the file changes
		fileWatcher.EnableRaisingEvents = true;
		fileWatcher.Changed += (s, e) => ReloadAssembly(e.FullPath);
	}

	private static void LoadAssembly(string path)
	{
		// Double check the change isn't the file being removed
		//! Pretty sure this won't happen for the changed event
		if (File.Exists(path) == false) return;

		// Make the assembly context thing
		// so we can keep track of it
		AssemblyLoadContext context = new AssemblyLoadContext(path, isCollectible: true);
		loadedAssemblies.Add(context);

		// Dynamically load the assembly
		//? Need to use stream because we replace the file (still using it)
		Stream assemblyBytes = new MemoryStream(File.ReadAllBytes(path));
		Assembly assembly = context.LoadFromStream(assemblyBytes);

		// Get the types of class in the script
		// TODO: Rewrite to be pretty
		Type scriptType = assembly.GetTypes()
		    .FirstOrDefault(type => typeof(IUpdatable).IsAssignableFrom(type) || typeof(IRenderable).IsAssignableFrom(type));

		// Turn the assembly into whatever object it is
		if (scriptType != null)
		{
			// Instantiate the object
			object newObject = Activator.CreateInstance(scriptType);

			// Cast and add to list depending on type
			if (newObject is IUpdatable logicScript) LoadedLogicScripts.Add(logicScript);
			if (newObject is IRenderable renderableScript) LoadedRenderableScripts.Add(renderableScript);
		}

		Console.WriteLine($"Loaded \"{Path.GetFileName(path)}\"");
	}

	private static void ReloadAssembly(string path)
	{
		// Find the assembly we wanna
		// reload then unload it
		AssemblyLoadContext oldContext = loadedAssemblies.Where(assembly => assembly.Name == path).FirstOrDefault();
		if (oldContext != null)
		{
			// Get rid of the old stuff
			SpringCleaning();

			// Unload the assembly and take
			// it out of circulation
			oldContext.Unload();
			loadedAssemblies.Remove(oldContext);
		}

		// Load the assembly again
		LoadAssembly(path);
	}

	// Remove all old instances of dynamically loaded assemblies
	private static void SpringCleaning()
	{
		// Run the clean methods on them to unload any
		// potentially previously loaded resources
		LoadedLogicScripts.ForEach(script => script.TidyUp());
		LoadedRenderableScripts.ForEach(script => script.TidyUp());

		// Clear the lists
		LoadedLogicScripts.Clear();
		LoadedRenderableScripts.Clear();
	}
}