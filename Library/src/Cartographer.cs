public class Cartographer
{
	public static Map GetMapFromName(string mapName)
	{
		Map currentMap = Project.Info.Maps.Where(map => map.Name == mapName).First();
		if (currentMap == null) throw new Exception($"can't find a map called '{mapName}' check spelling and case idk");

		return currentMap;
	}
}

public class Map
{
	public string Name { get; set; }
	public List<string> GameObjects { get; set; }
}