using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Smoke;

public static class ObjectManager
{
	public static List<GameObject> Prefabs = [];
	public static HashSet<Type> LoadedTypes = [];

	public static List<GameObject> DeserializeGameObjects(JArray rawGameObjects)
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

		// We're gonna return all the deserialized things
		List<GameObject> deserialized = new List<GameObject>();

		// Loop over all game objects for parsing/loading
		foreach (JObject rawGameObject in rawGameObjects)
		{
			// Get the game object, and remove the components
			// on it. When they're deserialized then we cannot
			// call start methods and whatnot so we gotta add
			// add them to the game object manually.
			// Also give the game object a new guild since guilds
			// aren't serialized inside the json
			// TODO: Make it so its cleared by default/we don't need to clear it
			//! might not need to do this idk
			GameObject currentGameObject = rawGameObject.ToObject<GameObject>(deserializer);
			currentGameObject.Guid = Guid.NewGuid();
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

			// Add the finished game object to the
			// list of deserialized game objects
			deserialized.Add(currentGameObject);
		}

		return deserialized;
	}





	public static GameObject PrefabFromDisplayName(string name)
	{
		return Prefabs.Where(gameObject => gameObject.DisplayName == name).FirstOrDefault();
	}

	public static GameObject PrefabFromGuid(Guid guid)
	{
		return Prefabs.Where(gameObject => gameObject.Guid == guid).FirstOrDefault();
	}
}