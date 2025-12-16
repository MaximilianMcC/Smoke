using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Camera
{
	private Camera3D camera;

	internal Camera3D AsRaylibVersion => camera;

	// protected internal override void Start()
	protected internal void Start()
	{
		camera = new Camera3D();
		camera.Projection = CameraProjection.Perspective;
		camera.Up = Vector3.UnitY;
	}
}