public class EntityManager
{
	public static Dictionary<Entity, List<IComponent>> Entities = new();

	public static Entity CreateEntity(string displayName)
	{
		// Make a new entity and
		// give it a random ID
		Entity entity = new Entity();
		entity.guid = Guid.NewGuid();
		entity.name = displayName;

		// Add the entity to the dictionary
		Entities.Add(entity, new List<IComponent>());
		return entity;
	}

	public static void AddComponentToEntity<T>(T component, Entity entity) where T : IComponent
	{
		Entities[entity].Add(component);
	}

	public static T GetComponent<T>(Entity entity) where T : IComponent
	{
		// TODO: Make it so you can do .GetComponent("transform") or something yk. use strings so you can name the components (easier to reference)
		return Entities[entity].OfType<T>().FirstOrDefault();
	}

	// TODO: instead do GetAllComponents(IComponent componentType)
	public static IEnumerable<(Entity, List<Script>)> GetAllScripts()
	{
		// TODO: Maybe don't use var (verbose)
		foreach (var entity in Entities)
		{
			List<Script> scripts = entity.Value.OfType<ScriptComponent>().Select(component => component.Script).ToList();
			yield return (entity.Key, scripts);
		}
	}
}



public class Entity
{
	public Guid guid;
	public string name;

	public List<IComponent> Components {
		get { return EntityManager.Entities[this]; }
	}

	public override string ToString() => $"{name} ({guid})";
}