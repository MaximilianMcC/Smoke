namespace Smoke;
public partial class Component
{
	/// <summary>
	/// This runs ONCE for the <i>entire</i> smoke game/project when the very first instance is created. Use it to load assets and stuff.
	/// <para>Method is executed before <c>Start()</c></para>
	/// </summary>
	protected internal virtual void LoadType() { }

	/// <summary>
	/// Runs once when the game object is spawned in.
	/// </summary>
	protected internal virtual void Start() { }

	/// <summary>
	/// Runs once every frame.
	/// </summary>
	protected internal virtual void Update() { }

	/// <summary>
	/// Runs once every frame after <c>Update()</c>.
	/// </summary>
	protected internal virtual void Render3D() { }

	/// <summary>
	/// Runs once every frame after <c>Update()</c>, and after <c>Render3D()</c>.
	/// </summary>
	protected internal virtual void Render2D() { }

	/// <summary>
	/// Runs once every frame after regular drawing.
	/// </summary>
	protected internal virtual void RenderDebug3D() { }

	/// <summary>
	/// Runs once every frame after regular drawing.
	/// </summary>
	protected internal virtual void RenderDebug2D() { }

	/// <summary>
	/// Runs when the current instance is killed/deleted/removed.
	/// </summary>
	protected internal virtual void CleanUp() { }

	/// <summary>
	/// This runs ONCE for the <i>entire</i> smoke game/project when it is closed. Use it to undo anything done in <c>LoadType()</c>
	/// </summary>
	protected internal virtual void CleanUpType() { }
}