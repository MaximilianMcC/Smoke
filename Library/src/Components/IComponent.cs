namespace Smoke;

public interface IComponent
{
	// Component logic (if it needs it)
	// Ran BEFORE a GameObjects Update
	public void Run();
}