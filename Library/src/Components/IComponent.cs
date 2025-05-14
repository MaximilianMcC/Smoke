namespace Smoke;

// TODO: Maybe split into updatable components, and renderable components, and like ones that don't even have methods ('fired' ones)
public interface IComponent
{
	// Component logic (if it needs it)
	// Ran BEFORE a GameObjects Update
	public void Run();
}