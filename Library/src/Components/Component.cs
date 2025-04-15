using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

public interface IComponent { }

public class AdvancedComponentLoader
{
	private static Assembly assembly;

	public static void Init()
	{
		// Get the path to the DLL file
		//? This dll has all the game code in it
		string dllFile = Project.Info.Name + ".dll";
		string dllPath = Path.Join(Project.RootPath, "bin", "assemblies", dllFile);

		// Dynamically load the assembly from the DLL
		assembly = Assembly.LoadFrom(dllPath);
	}

	//! scripts MUST be last in the components list of json
	public static void LoadScript(Entity entity, IComponent component)
	{
		// Treat the component as a script
		ScriptComponent scriptComponent = component as ScriptComponent;

		// Load the script
		// TODO: Don't load if the script/class has previously been loaded
		Type scriptType = assembly.GetType(scriptComponent.ClassPath);
		if (scriptType == null) return;

		// Actually get/make the script
		Script script = Activator.CreateInstance(scriptType) as Script;
		script.Entity = entity;

		// Put the script onto the entity
		scriptComponent.Script = script;
		EntityManager.AddComponentToEntity(scriptComponent, entity);

		// Setup the script
		script.InitialSetup();
	}
}