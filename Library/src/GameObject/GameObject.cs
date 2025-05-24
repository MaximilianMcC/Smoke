using System.Reflection;

namespace Smoke;

public class GameObject
{
	// TODO: Maybe make a prefab bool idk

	public string DisplayName;
	public Guid Guid;
	public List<Component> Components = [];

	public void Add(Component component)
	{
		// Set ourself to be a parent
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

		// Call the load method (only happens once per TYPE)
		if (ObjectManager.LoadedTypes.Contains(component.GetType()) == false)
		{
			component.LoadType();
			ObjectManager.LoadedTypes.Add(component.GetType());
		}
	}

	// TODO: Make it so components have a name so you can get them based on that (not just first (ts))
	public T Get<T>() where T : Component
	{
		return Components.OfType<T>().FirstOrDefault();
	}

	public void Start()
	{
		// Loop over all eligible components
		// and run their start method
		// TODO: Make sure the load method of a component is ran
		foreach (UpdatableComponent component in Components.OfType<UpdatableComponent>())
		{
			component.Start();
		}
	}

	public void TidyUp()
	{
		// Loop over all eligible components
		// and run their tidy up method
		foreach (UpdatableComponent component in Components.OfType<UpdatableComponent>())
		{
			component.TidyUp();
		}
	}

	public void RemoveFromScene()
	{
		// Call the tidy up method
		TidyUp();

		// Remove ourself
		SceneManager.CurrentScene.Things.Remove(this);
	}

	public override string ToString() => $"{DisplayName} ({Guid})";
}