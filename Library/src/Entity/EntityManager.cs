using System.Text.Json;
using Force.DeepCloner;

public class EntityManager
{
	public static Dictionary<Entity, List<IComponent>> Entities = [];
	public static List<Entity> InstancedEntities = [];

	// Another shortcut thing
	public static Entity CreateFromPrefab(string prefabName, string displayName = null)
	{
		return CreateFromPrefab(GetPrefabFromName(prefabName), displayName);
	}

	public static Entity CreateFromPrefab(Prefab prefab, string displayName = null)
	{
		// Check for if we actually have a prefab
		if (prefab == null) return null;

		// Make a new entity for the prefab
		Entity entity = new Entity()
		{
			guid = Guid.NewGuid(),
			name = displayName ?? prefab.DisplayName
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
	public static void CreateAndSpawnPrefab(Prefab prefab, string prefabName = null)
	{
		Spawn(CreateFromPrefab(prefab, prefabName));
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

	public static Prefab GetPrefabFromName(string displayName)
	{
		// Get any prefabs with the name we want (returns null if not a thing)
		return Project.Info.Prefabs.Concat(Project.Info.CurrentMap.InstancedPrefabs).Where(prefab => prefab.DisplayName == displayName).First();
	}
}