namespace Smoke;
public class Scene
{
	public string Name;
	
	public List<GameObject> RootGameObjects;

	private List<GameObject> cachedGameObjects;
	public List<GameObject> GameObjects
	{
	    get
	    {
			//? ??= assigns only if the value is already null
	        cachedGameObjects ??= GetAllGameObjects();
	        return cachedGameObjects;
	    }
	}


	// Thing that gives back ALL game objects (children) in a 'normal' 1D list
	private List<GameObject> GetAllGameObjects()
	{
		List<GameObject> allGameObjects = [];

		// Loop over all root game objects
		foreach (GameObject gameObject in RootGameObjects)
		{
			// Get all its children and add it to the list
			AddChildrenToList(allGameObjects, gameObject);
		}

		return allGameObjects;
	}

	private void AddChildrenToList(List<GameObject> list, GameObject parent)
	{
		// Add ourselves
		list.Add(parent);

		// Add our children
		parent.Children.ForEach(child => AddChildrenToList(list, child));
	}

	// When the GameObjects list is edited (added/removed)
	// then this will recache the list of game objects
	// next time someone tries to use the getter thing
	private void RequestGameObjectsRecache() => cachedGameObjects = null;


	//! DON'T do this AT ALL
	// TODO: rewrite.
	public void Add(GameObject newGameObject)
	{
		RequestGameObjectsRecache();
		RootGameObjects.Add(newGameObject);
	}


	internal void Start()
	{
		// Load all the initial game objects
		foreach (GameObject gameObject in GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.InternalLoadType();
				component.Start();
			}
		}
	}

	//! this is like the third chain of these so lowk don't do this
	internal void Update()
	{
		foreach (GameObject gameObject in GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.InternalLoadType();
				component.Start();
			}
		}
	}

	internal void Render3D()
	{
		foreach (GameObject gameObject in RootGameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.Render3D();
			}
		}
	}

	internal void Render2D()
	{
		foreach (GameObject gameObject in RootGameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.Render2D();
			}
		}
	}

	internal void Unload()
	{
		foreach (GameObject gameObject in RootGameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.CleanUp();
			}
		}
	}
}