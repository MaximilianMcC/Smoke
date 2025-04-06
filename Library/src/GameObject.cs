// TODO: Make everything in here getters (not setters)
public class GameObject
{
	// Just used for in the hierarchy thing
	string DisplayName { get; set; }

	// Unique to every instance of this
	ulong Id { get; set; }

	// Stuff
	List<GameObjectComponent> Components { get; set; }
	List<string> ScriptPaths { get; set; }
}

class GameObjectComponent
{
	public string Type;
}