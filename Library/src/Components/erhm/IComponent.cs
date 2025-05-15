namespace Smoke;

public interface IComponent;

// Literally just a container
public interface IFixedComponent;

// Ran during rendering
public interface IRenderableComponent;

// Ran before `Update()`
public interface IUpdatableComponent
{
	public void Update();
}
