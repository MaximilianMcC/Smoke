namespace Smoke;
public class Scene
{
	public string Name;	
	public List<GameObject> RootGameObjects;

	//! DON'T do this AT ALL
	// TODO: rewrite.
	internal void Add(GameObject newGameObject)
	{
		RootGameObjects.Add(newGameObject);
	}

	// TODO: rewrite. maybe
	// TODO: Don't do at all ngl
	internal void Add(GameObject newGameObject, GameObject parent)
	{
		parent.Children.Add(newGameObject);
	}

	internal void Start()
	{
		RootGameObjects.ForEach(gameObject => gameObject.Start());
	}

	internal void Update()
	{
		RootGameObjects.ForEach(gameObject => gameObject.Update());
	}

	internal void Render3D()
	{
		RootGameObjects.ForEach(gameObject => gameObject.Render3D());
	}

	internal void Render2D()
	{
		RootGameObjects.ForEach(gameObject => gameObject.Render2D());
	}

	internal void Unload()
	{
		RootGameObjects.ForEach(gameObject => gameObject.CleanUp());
	}
}