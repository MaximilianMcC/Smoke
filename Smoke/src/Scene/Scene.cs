namespace Smoke;
public class Scene
{
	public string Name;
	
	public List<GameObject> GameObjects;

	internal void Start()
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

	internal void Update()
	{
		foreach (GameObject gameObject in GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.Update();
			}
		}
	}

	internal void Render3D()
	{
		foreach (GameObject gameObject in GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.Render3D();
			}
		}
	}

	internal void Render2D()
	{
		foreach (GameObject gameObject in GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.Render2D();
			}
		}
	}

	internal void Unload()
	{
		foreach (GameObject gameObject in GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.CleanUp();
			}
		}
	}
}