public interface IScript
{
	void Update(Entity entity);
}

public class ScriptComponent : Component
{
	public string Url;
	public IScript Script;
}

class ScriptSystem : ISystem
{
	public void Run()
	{
		// Loop through all script components and run their update
		foreach ((Entity entity, ScriptComponent script) in EntityManager.GetAllEntitiesWithComponent<ScriptComponent>())
		{
			script.Script.Update(entity);
		}
	}
}