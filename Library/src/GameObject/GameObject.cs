namespace Smoke;

public class GameObject
{
	public string DisplayName;
	public Guid Guid;
	public List<Component> Components = [];

	public GameObject(string displayName = null)
	{
		// Add a display name (if given) and a guid
		Guid = new Guid();
		DisplayName = displayName ?? "john";

		// Add ourself to the game objects list
		GameObjectManager.GameObjects.Add(this);

		// All game objects come with a mandatory transform
		AddComponent(new Transform());
	}

	public void AddComponent(Component componentToAdd)
	{
		// Set ourself to be the components
		// parent and add it to ourself also
		componentToAdd.ParentGameObject = this;
		Components.Add(componentToAdd);

		// Inject the components variables
		// and run its start method
		componentToAdd.InjectComponents();
		componentToAdd.Start();
	}

	// Two ways of getting components
	public T GetComponent<T>() where T : Component => Components.Find(component => component is T) as T;
	public Component GetComponent(Type type) => Components.Find(component => type.IsInstanceOfType(component));

	public void Update()
	{
		// Loop through all components and update them
		foreach (Component component in Components)
		{
			component.Update();
		}
	}

	public void TidyUp()
	{
		// Leave the game object list
		GameObjectManager.GameObjects.Remove(this);
	}
}