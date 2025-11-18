using Newtonsoft.Json;

namespace Smoke;
public class GameObject
{
	// A game object is nothing more than a container
	public List<Component> Components;

	// Remove constructors
	private GameObject() { }
}