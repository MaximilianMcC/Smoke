using System.Reflection;

class GameObjectLoader
{
	private static Assembly assembly;

	public static void Init()
	{
		// Get the path to the DLL file
		//? This dll has all the game code in it
		string dllFile = Project.Info.Name + ".dll";
		string dllPath = Path.Join(Project.RootPath, "bin", "assemblies", dllFile);

		// Dynamically load the assembly from the DLL
		Console.WriteLine("Loading stuff from " + dllPath);
		assembly = Assembly.LoadFrom(dllPath);

		foreach (Type type in assembly.GetTypes())
		{
			Console.WriteLine("🖲️ " +  type.FullName);
		}
	}

	public static void LoadAllGameObjects()
	{
		// Loop through all game objects and load them
		Project.Info.GameObjects.ForEach(LoadGameObject);
	}

	// Load all components of a game object
	private static void LoadGameObject(GameObject gameObject)
	{
		// Make the entity
		Entity entity = EntityManager.CreateEntity(gameObject.DisplayName);
		Console.WriteLine("🧑‍🦯‍➡️ " + entity);

		// Load/apply all components
		foreach (IComponent component in gameObject.Components)
		{
			// If it's a "special" component then handle accordingly
			// TODO: switch
			if (component is ScriptComponent) LoadScript(entity, component);
			else
			{
				// Kinda "normal" component
				EntityManager.AddComponentToEntity(component, entity);
			}
		}

		Console.WriteLine("🚶‍➡️ " + entity);
	}

	private static void LoadScript(Entity entity, IComponent component)
	{
		Console.WriteLine("Loading script");
		ScriptComponent scriptComponent = component as ScriptComponent;

		// Load the script
		// TODO: Don't load if the script/class has previously been loaded
		Type scriptType = assembly.GetType(scriptComponent.ClassPath);
		if (scriptType == null) return;

		// Actually get/make the script
		Script script = Activator.CreateInstance(scriptType) as Script;
		scriptComponent.Script = script;

		// Put the script onto the entity
		EntityManager.AddComponentToEntity(scriptComponent, entity);
	}
}