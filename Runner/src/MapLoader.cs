class MapLoader
{
	public static void LoadMap(string mapName)
	{
		// Get the map
		Map map = Cartographer.GetMapFromName(mapName);

		// Loop through all game objects in the map
		foreach (string gameObjectGuid in map.GameObjects)
		{
			// Load the current game object
			GameObject gameObject = Project.Info.GameObjects.Where(gameObject => gameObject.Guid == gameObjectGuid).First();
			if (gameObject == null) throw new Exception($"Can't find a game object with the GUID {gameObjectGuid}");

			// Actually load the thing
			GameObjectLoader.LoadGameObject(gameObject);
		}
	}
}