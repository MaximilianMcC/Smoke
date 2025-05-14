using System.Numerics;

public class ScriptComponent : IComponent
{
	public string ClassPath { get; set; }

	// TODO: Json ignore maybe but also never gonna be reading from this so like all goods
	public Script Script;
}

public class Script
{
	public Entity Entity;
	public Transform Transform;

	// Set up shorthands and stuff yk
	//! do NOT run this more than once ()
	public virtual void InitialSetup()
	{
		// Just get a heap of components ig
		Transform = GetComponent<Transform>();

		// Run the initialization method. This is where
		// assets can be loaded and whatnot. Only happens
		// a single time for the whole script
		ScriptInitialization();
		firstTimeRunningScript = true;
	}

	// Will only be called once
	private static bool firstTimeRunningScript = false;
	public virtual void ScriptInitialization() {  }

	public virtual void Start() {  }
	public virtual void Update() {  }
	public virtual void Render2D() {  }
	public virtual void Render3D() {  }
	public virtual void RenderDebug2D() {  }
	public virtual void RenderDebug3D() {  }
	public virtual void TidyUp() {  }

	// Just saves you calling on EntityManger (qol)
	// TODO: Maybe make protected
	public T GetComponent<T>() where T : IComponent
	{
		return Entity.GetComponent<T>();
	}

	// Remove the thing from the game but not entities list
	//? ngl might be fully useless
	public void Deinstance()
	{
		EntityManager.InstancedEntities.Remove(Entity);
	}

	// fully get rid of the thing (never coming back)
	public void Eradicate()
	{
		TidyUp();
		
		EntityManager.InstancedEntities.Remove(Entity);
		EntityManager.Entities.Remove(Entity);
	}

	public override string ToString() => Entity.ToString();
}