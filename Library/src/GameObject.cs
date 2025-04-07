// TODO: Make everything in here getters (not setters)
public class GameObject
{
	// Just used for in the hierarchy thing
	public string DisplayName { get; set; }

	// Unique to every instance of this
	public ulong Id { get; set; }

	// Stuff
	public List<Component> Components { get; set; }
	public List<string> ScriptPaths { get; set; }
}

public class GameObjectComponent
{
	public string Type { get; set; }
}

