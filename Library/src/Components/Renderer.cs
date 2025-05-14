namespace Smoke;

public class Renderer : IComponent
{
	// 2D Rendering stuff
	public void Render2D() { }
	public void RenderDebug2D() { }

	// 3D Rendering stuff
	public void Render3D() { }
	public void RenderDebug3D() { }

	// TODO: Maybe call them? Also like only do the debug ones if debug mode is on
	public void Run() { }
}