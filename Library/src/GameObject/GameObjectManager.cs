namespace Smoke;

public class GameObjectManager
{
	public static List<GameObject> GameObjects = [];

	public static GameObject FromGuid(Guid guid)
	{
		return GameObjects.Where(gameObject => gameObject.Guid == guid).FirstOrDefault();
	}
}