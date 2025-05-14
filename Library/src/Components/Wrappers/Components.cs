namespace Smoke;

public abstract class ComponentBase : IComponent
{
	// TODO: Rename to parent
	public GameObject Owner { get; set; }

	protected ComponentBase(GameObject owner)	
	{
		// Assign the owner
		Owner = owner;
	}
}