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

		// Get all fixed components
		gameObject.FixedComponents = properties
			.Select(property => property.GetValue(gameObject))
			.OfType<IFixedComponent>()
			.ToArray();

		// Get all rendering components
		gameObject.RenderableComponents = properties
			.Select(property => property.GetValue(gameObject))
			.OfType<IRenderableComponent>()
			.ToArray();

		// Get all updatable components
		gameObject.UpdatableComponents = properties
			.Select(property => property.GetValue(gameObject))
			.OfType<IUpdatableComponent>()
			.ToArray();
	}
}