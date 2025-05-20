using Force.DeepCloner;

namespace Smoke;

public class Scene
{
	public string DisplayName;
	public Guid Guid;
	public List<GameObject> Things = [];

	public GameObject CreatePrefab(string prefabDisplayName, string newPrefabsDisplayName) => CreatePrefab(ObjectManager.PrefabFromDisplayName(prefabDisplayName), newPrefabsDisplayName);
	public GameObject CreatePrefab(Guid prefabGuid, string newPrefabsDisplayName) => CreatePrefab(ObjectManager.PrefabFromGuid(prefabGuid), newPrefabsDisplayName);
	public GameObject CreatePrefab(GameObject prefabGameObject, string newPrefabsDisplayName)
	{
		// Copy the prefab so we create a new game object
		GameObject newGameObject = prefabGameObject.DeepClone();

		// Give it a new guid since this is a unique
		// object and add the display name 
		// TODO: Add a name and make it add like whatever (x) where x is the number of the unnamed prefabs yk 
		newGameObject.Guid = new Guid();
		newGameObject.DisplayName = newPrefabsDisplayName;

		// Add the new game object to the scene
		// and call all its start methods
		Things.Add(newGameObject);
		newGameObject.Start();

		// Return it because most likely you wanna modify it yk
		return newGameObject;
	}

	public GameObject Get(string prefabDisplayName) => Things.Find(thing => thing.DisplayName == prefabDisplayName);
	public GameObject Get(Guid prefabGuid) => Things.Find(thing => thing.Guid == prefabGuid);
}