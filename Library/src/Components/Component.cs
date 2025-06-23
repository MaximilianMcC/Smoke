using Newtonsoft.Json;

namespace Smoke;

public abstract class Component
{
	// TODO: Assign a LoadPriority normalised float (0-1) that determines if it needs to be loaded before other stuff (for example PlayerMovement would be loaded last, with textures and timers and stuff loaded first because PlayerMovement depends on them)

	//? This has to be ignored otherwise its an endless loop
	// TODO: Put GUID as a string here idk
	[JsonIgnore]
	public GameObject GameObject;

	// Runs ONCE for the entire game when the VERY FIRST
	// instance of this type has been created. Use for
	// loading assets and whatnot yk
	// TODO: Add an upload type
	public virtual void LoadType() { }
}

public abstract class UpdatableComponent : Component
{
	public bool Enabled = true;

	public virtual void Start() { }
	public virtual void Update() { }
	public virtual void TidyUp() { }
}

public abstract class RenderableComponent : UpdatableComponent
{
	public virtual void Render2D() { }
	public virtual void RenderDebug2D() { }

	public virtual void Render3D() { }
	public virtual void RenderDebug3D() { }
}