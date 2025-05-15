using Newtonsoft.Json;

namespace Smoke;

[JsonObject(MemberSerialization.Fields)]
public class Renderer : IRenderableComponent
{
	// 2D Rendering stuff
	public void Render2D() { }
	public void RenderDebug2D() { }

	// 3D Rendering stuff
	public void Render3D() { }
	public void RenderDebug3D() { }
}