using Newtonsoft.Json;

namespace Smoke;
public class GameObject
{
	// A game object is nothing more than a container
	public List<Component> Components = [];
	public List<GameObject> Children = [];
	public GameObject Parent = null;
	public Guid Guid { get; private set; }

	public string DisplayName = "GameObject";

	// Remove constructors for the user
	//? you're also not allowed to use them btw max
	internal GameObject()
	{
		// Give the game object an id
		Guid = Guid.NewGuid();
	}

	private void ForAllComponents(Action<Component> action)
	{
		// Handle our components
		foreach (Component component in Components)
		{
			action(component);
		}

		// Handle our children's components
		foreach (GameObject child in Children)
		{
			child.ForAllComponents(action);
		}
	}

	internal void Start()
	{
		ForAllComponents(component =>
		{
			component.InternalLoadType();
			component.Start();
		});
	}

	internal void Update()
	{
		ForAllComponents(component => component.Update());
	}

	internal void Render3D()
	{
		ForAllComponents(component => component.Render3D());
	}

	internal void Render2D()
	{
		ForAllComponents(component => component.Render2D());
	}

	internal void RenderDebug3D()
	{
		ForAllComponents(component => component.InternalRenderDebug3D());
	}

	internal void RenderDebug2D()
	{
		ForAllComponents(component => component.InternalRenderDebug2D());
	}

	internal void CleanUp()
	{
		ForAllComponents(component => component.CleanUp());
	}

	internal void CleanUpType()
	{
		ForAllComponents(component => component.CleanUpType());
	}


	
	// Component access stuff
	// TODO: Make a TryGetComponent that return a bool and has an out value
	public T GetComponent<T>() => Components.OfType<T>().FirstOrDefault();
	public List<T> GetComponents<T>() => Components.OfType<T>().ToList();
}