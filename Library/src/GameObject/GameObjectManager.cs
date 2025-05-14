using System.Reflection;

namespace Smoke;

public class GameObjectManager
{
	public static List<GameObject> GameObjects = new List<GameObject>();

	public static void CacheComponents(GameObject gameObject)
	{
		// Get all properties so we can sift
		// through and find all components
		PropertyInfo[] properties = gameObject.GetType().GetProperties();

		// Get all components
		gameObject.Components = properties
			.Select(property => property.GetValue(gameObject))
			.OfType<IComponent>()
			.ToArray();
	}
}