using System.Reflection;
using Newtonsoft.Json;

namespace Smoke;

public class GameObjectManager
{
	public static List<GameObject> GameObjects = new List<GameObject>();

	// Load a game object from JSON
	// TODO: Take a JSON node or something yk (no string)
	public static void DeserializeGameObject(string gameObjectJson)
	{
		// TODO: Maybe get rid of indentation to discourage manual editing
		JsonSerializerSettings jsonSettings = new JsonSerializerSettings()
		{
			TypeNameHandling = TypeNameHandling.All,
			Formatting = Formatting.Indented
		};

		// Parse the game object
		GameObject deserializedGameObject = JsonConvert.DeserializeObject<GameObject>(gameObjectJson, jsonSettings);

		// Cache its component member
		// things and chuck it in the
		// game objects list
		// TODO: Don't add to list
		CacheComponents(deserializedGameObject);
		GameObjects.Add(deserializedGameObject);
	}

	private static void CacheComponents(GameObject gameObject)
	{
		// Get all properties so we can sift
		// through and find all components
		PropertyInfo[] properties = gameObject.GetType().GetProperties();

		// Get all fixed components
		gameObject.FixedComponents = properties
			.Select(property => property.GetValue(gameObject))
			.Where(component => component is IFixedComponent)
			.Cast<IFixedComponent>()
			.ToArray();

		// Get all rendering components
		gameObject.RenderableComponents = properties
			.Select(property => property.GetValue(gameObject))
			.Where(component => component is IRenderableComponent)
			.Cast<IRenderableComponent>()
			.ToArray();

		// Get all updatable components
		gameObject.UpdatableComponents = properties
			.Select(property => property.GetValue(gameObject))
			.Where(component => component is IUpdatableComponent)
			.Cast<IUpdatableComponent>()
			.ToArray();		
	}
}