using Newtonsoft.Json;

namespace Smoke;
public class GameObject
{
	// A game object is nothing more than a container
	public List<Component> Components = [];
	public List<GameObject> Children = [];

	public string DisplayName = "GameObject";

	// Remove constructors for the user
	//? you're also not allowed to use them btw
	internal GameObject() { }
}