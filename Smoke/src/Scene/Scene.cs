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

	internal void Unload()
	{
		//? Apparently a nested foreach is quicker than linq
		foreach (GameObject gameObject in GameObjects)
		{
			foreach (Component component in gameObject.Components)
			{
				component.CleanUp();
			}
		}
	}
}