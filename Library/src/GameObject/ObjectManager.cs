using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Smoke;

public class ObjectManager
{
	public static List<GameObject> Prefabs = [];
	public static List<GameObject> Instanced = [];
	public static HashSet<Type> LoadedTypes = [];
	

	public static void DeserializeObjects(JArray rawGameObjects)
	{
		// Get the fancy json settings so we can properly
		// pass the objects to the correct type/namespace
		JsonSerializerSettings settings = new JsonSerializerSettings()
		{
			// Stuff for assemblies
			TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple,
			SerializationBinder = new DefaultSerializationBinder(),

			// Defines the datatype in a "$type" property
			TypeNameHandling = TypeNameHandling.Auto,

			// Tabs
			Formatting = Formatting.Indented
		};
		JsonSerializer deserializer = JsonSerializer.Create(settings);


		// Loop over all game objects for parsing/loading
		foreach (JObject rawGameObject in rawGameObjects)
		{
			// Get the game object, and remove the components
			// on it. When they're deserialized then we cannot
			// call start methods and whatnot so we gotta add
			// add them to the game object manually
			// TODO: Make it so its cleared by default/we don't need to clear it
			//! might not need to do this idk
			GameObject currentGameObject = rawGameObject.ToObject<GameObject>(deserializer);
			currentGameObject.Components.Clear();

			// Get the components
			JArray rawComponents = (JArray)rawGameObject["Components"];
			foreach (JObject rawComponent in rawComponents)
			{
				// Parse the component then add
				// it to current game object
				Component currentComponent = rawComponent.ToObject<Component>(deserializer);
				currentGameObject.Add(currentComponent);
			}

			// Add the finished game object to the list of prefabs
			Prefabs.Add(currentGameObject);
		}
		


		// // Loop through all components and reassign
		// // their parent game objects because they
		// // are lost during serialization to json
		// foreach (GameObject gameObject in Prefabs)
		// {
		// 	foreach (Component component in gameObject.Components)
		// 	{
		// 		component.GameObject = gameObject;
		// 	}
		// }
	}



	public static GameObject FromGuid(Guid guid)
	{
		return Prefabs.Where(gameObject => gameObject.Guid == guid).FirstOrDefault();
	}
}