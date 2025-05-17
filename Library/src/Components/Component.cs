using System.Reflection;
using Newtonsoft.Json;
using Smoke;

// Basically the script script class from before
public abstract class Component
{
	//? Makes an endless loop if this is not here
	[JsonIgnore]
	public GameObject ParentGameObject;

	public void InjectComponents()
	{
		// Find all public variables we have rn
		FieldInfo[] variables = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		foreach (FieldInfo field in variables)
		{
			// Check for if the current variable
			// comes from a component (us rn)
			if (typeof(Component).IsAssignableFrom(field.FieldType) == false) continue;

			// See if the parent has any components
			// that are the same as the variable
			// we're looking at rn
			Component component = ParentGameObject.GetComponent(field.FieldType);
			if (component != null)
			{
				// Basically just steal the
				// component & put it on us
				field.SetValue(this, component);
			}
		}
	}

	public virtual void Start() { }
	public virtual void Update() { }
	public virtual void TidyUp() { }

	public virtual void Render2D() { }
	public virtual void RenderDebug2D() { }
	public virtual void Render3D() { }
	public virtual void RenderDebug3D() { }
}