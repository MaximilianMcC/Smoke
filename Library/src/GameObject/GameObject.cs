using System.Reflection;

namespace Smoke;

public class GameObject
{
	// TODO: Make this readonly somehow
	public IFixedComponent[] FixedComponents;
	public IRenderableComponent[] RenderableComponents;
	public IUpdatableComponent[] UpdatableComponents;

	// TODO: Get rid of the 'when' & 'on' rubbish maybe
	public virtual void WhenSpawned() { }
	public virtual void OnUpdate() { }
	public virtual void TidyUp() { }
}