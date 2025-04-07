public class EntityManager
{
	public static List<Entity> Entities = new List<Entity>();
	public static Dictionary<Entity, List<Component>> EntityComponents = new();

	public static Entity CreateEntity()
	{
		// Make a new entity and
		// give it a random ID
		Entity entity = new Entity();
		entity.guid = Guid.NewGuid();

		// Make a new list to store the
		// entities components in
		//? Not in the entity class because it more performant (stop us from looping through everything and whatnot) and reinforces the idea of entities USING components, not HAVING components
		EntityComponents.Add(entity, new List<Component>());

		// Add the entity to the list
		// of them all, and give it back
		Entities.Add(entity);
		return entity;
	}

	public static void AddComponent(Component component, Entity entity)
	{
		EntityComponents[entity].Add(component);
	}

	public static T GetComponent<T>(Entity entity) where T : Component
	{
		// TODO: Make it so you can do .GetComponent("transform") or something yk. use strings so you can name the components (easier to reference)
		return EntityComponents[entity].OfType<T>().FirstOrDefault();
	}

	public static IEnumerable<(Entity, T)> GetAllEntitiesWithComponent<T>() where T : Component
	{
		// Loop over all entities
		foreach (Entity entity in Entities)
		{
			// Get all components on the entity
			IEnumerable<T> components = EntityComponents[entity].OfType<T>();
			foreach (T currentComponent in components)
			{
				// Give back the component and the entity that has it
				yield return (entity, currentComponent);
			}
		}
	}
}



public class Entity
{
	public Guid guid;
}