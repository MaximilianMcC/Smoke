using System.Net.Http.Headers;
using System.Reflection;

namespace Smoke;

public class GameObject
{
	public string DisplayName;
	public Guid Guid;
	public List<Component> Components = [];

	public GameObject(string displayName = null)
	{
		// Add a display name (if given) and a guid
		Guid = Guid.NewGuid();
		DisplayName = displayName ?? "john";

		// Add ourself to the game objects list
		GameObjectManager.GameObjects.Add(this);
	}

	public void Add(Component component)
	{
		// Set ourself to be the components
		// parent and add it to ourself also
		component.GameObject = this;
		Components.Add(component);

		// 'Inject' the components' variables
		//? field is a variable defined not in a method btw (ones where you chuck the access modifier on it yk)
		//! FieldInfo[] variables = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
		FieldInfo[] variables = GetType().GetFields(BindingFlags.Public);
		foreach (FieldInfo field in variables)
		{
			// If we're looking at a component then
			// steal all the variables from it and put
			// them into this new component
			if (field.FieldType.IsAssignableFrom(component.GetType()))
			{
				// Inject the variable
				field.SetValue(this, component);
			}
		}

		// Call the start method if applicable
		if (component is UpdatableComponent updatable) updatable.Start();
	}

	// TODO: Make it so components have a name so you can get them based on that (not just first (ts))
	public T Get<T>() where T : Component
	{
		return Components.OfType<T>().FirstOrDefault();
	}

	public void TidyUp()
	{
		// Leave the game object list
		GameObjectManager.GameObjects.Remove(this);
	}
}