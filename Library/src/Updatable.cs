/*
	This basically goes on everything that needs to run
	constantly every frame, opposed to just like a single
	time or something like that. Basically all objects.
*/

/// <summary>
/// Something that can be manipulated each frame. Normally attached to an object of sorts.
/// </summary>
public interface IUpdatable
{
	/// <summary>
	/// Runs a single time when the thing this script is on spawns in
	/// </summary>
	void Start() {}

	/// <summary>
	/// Runs once every frame
	/// </summary>
	void Update() {}

	/// <summary>
	/// Render 3D things in here
	/// </summary>
	void Render3D() {}

	/// <summary>
	/// 3D things rendered in here will only be visible when debug mode is on
	/// </summary>
	void RenderDebug3D() {}

	/// <summary>
	/// Render 2D things in here
	/// </summary>
	void Render2D() {}

	/// <summary>
	/// 2D things rendered in here will only be visible when debug mode is on
	/// </summary>
	void RenderDebug2D() {}

	/// <summary>
	/// Get rid of any loaded in things here. Runs once before the object is removed. Be a tidy kiwi
	/// </summary>
	void TidyUp() {}
}