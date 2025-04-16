using System.Text.Json;
using Force.DeepCloner;

public class EntityManager
{
	public static Dictionary<Entity, List<IComponent>> Entities = [];
	public static List<Entity> InstancedEntities = [];

	public static Entity CreateFromPrefab(string prefabGuid, string prefabName = null)
	{
		// Get/find the prefab we're copying from
		Prefab prefab = Project.Info.Prefabs.Concat(Project.Info.CurrentMap.InstancedPrefabs).FirstOrDefault(prefab => prefab.Guid == prefabGuid);
		if (prefab == null) return null;

		// Make a new entity for the prefab
		Entity entity = new Entity()
		{
			guid = Guid.NewGuid(),
			name = prefabName ?? prefab.DisplayName
		};

		// Add to the list of ALL entities
		Entities.Add(entity, new List<IComponent>());

		// Add all components from the prefab onto the new entity
		foreach (IComponent component in prefab.Components)
		{
			// Make a clone of the component for this unique entity
			IComponent currentComponent = component.DeepClone();

			// If it's a "special" component then handle accordingly
			// TODO: switch
			if (currentComponent is ScriptComponent)
			{
				// Load a script onto the entity
				AdvancedComponentLoader.LoadScript(entity, currentComponent);
			}
			else
			{
				// Chuck the standard component onto the entity
				AddComponentToEntity(currentComponent, entity);
			}
		}

		// Give back the entity
		return entity;
	}

	// // TODO: Put somewhere else
	// private static IComponent CloneComponent(IComponent component)
	// {
	// 	// Serialize the component into JSON (getting rid of any personal identity)
	// 	string json = JsonSerializer.Serialize(component, component.GetType());
		
	// 	// Create a brand new component by parsing it
	// 	return JsonSerializer.Deserialize(json, component.GetType()) as IComponent;
	// }

	// Instance an entity (actually put it in the game)
	// TODO: Maybe make bool and return if it added it or not (already exists)
	public static void Spawn(Entity entity)
	{
		// Add to the list of entities IN the game
		InstancedEntities.Add(entity);

		// Run any start methods the entity has
		// TODO: Make it work for multiple scripts, not just one
		ScriptComponent script = GetComponent<ScriptComponent>(entity);
		if (script != null) script.Script.Start();
	}

	// Avoids calling the thingy twice (for if its simple as)
	public static void CreateAndSpawnPrefab(string prefabGuid, string prefabName = null)
	{
		Spawn(CreateFromPrefab(prefabGuid, prefabName));
	}


	public static void AddComponentToEntity<T>(T component, Entity entity) where T : IComponent
	{
		Entities[entity].Add(component);
	}

	// TODO: Make it work for multiple components, not just one
	// TODO: return bool, return via out
	public static T GetComponent<T>(Entity entity) where T : IComponent
	{
		// TODO: Make it so you can do .GetComponent("head transform") or something yk. use strings so you can name the components (easier to reference)
		T component = Entities[entity].OfType<T>().FirstOrDefault();
		if (EqualityComparer<T>.Default.Equals(component, default))
			throw new NullReferenceException($"There isn't a {typeof(T).Name} component on the current entity");
		
		return component;
	}

	public static IEnumerable<T> GetComponents<T>(Entity entity) where T : IComponent
	{
		// Get all components of the requested type
		List<T> components = Entities[entity].OfType<T>().ToList();

		// Give each back
		// TODO: Maybe don't use todo idk
		foreach (T component in components) yield return component;
	}
}



public class Entity
{
	public Guid guid;
	public string name;

	public override string ToString() => $"{name} ({guid})";
}