using System.Reflection;

class GameObjectLoader
{
	private static Assembly assembly;

	public static void Init()
	{
		// Get the path to the DLL file
		//? This dll has all the game code in it
		string dllPath = Path.Join(Project.RootPath, "bin", "assemblies");

		// Dynamically load the assembly from the DLL
		assembly = Assembly.LoadFrom(dllPath);
	}

	public static void LoadAllGameObjects()
	{
		// Loop through all game objects and load them
		Project.Info.gameObjects.ForEach(gameObject => LoadGameObject(gameObject));
	}

	// Load all components of a game object
	private static void LoadGameObject(GameObject gameObject)
	{
		// Make an entity for the game object
		Entity entity = EntityManager.CreateEntity();

		// Loop through all components
		foreach (Component component in gameObject.Components)
		{

			// Check for what component we're
			// dealing with and load accordingly
			// TODO: Put the load methods in the same class as the components
			switch (component.Type)
			{
				case "Script":
					LoadScript(entity, (ScriptComponent)component);
					break;
				
				case "Transform":
					LoadTransform(entity, (Transform)component);
					break;
			}
		}
	}

	private static void LoadScript(Entity entity, ScriptComponent component)
	{
		// Load the script
		// TODO: Don't if the script has previously been loaded
		Type scriptType = assembly.GetType($"{Project.Info.Name}.{component.Url}");
		if (scriptType == null) return;

		// Actually get/make the script
		IScript script = Activator.CreateInstance(scriptType) as IScript;
		component.Script = script;

		// Put the script onto the entity
		EntityManager.AddComponent(component, entity);
	}

	private static void LoadTransform(Entity entity, Transform component)
	{
		// Put the transform onto the entity
		EntityManager.AddComponent(component, entity);
	}
}