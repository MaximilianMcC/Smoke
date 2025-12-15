using System.Numerics;
using Raylib_cs;

namespace Smoke;
public abstract class Camera : Component;

public class Camera3D : Camera
{
	// TODO: Take into account parents
	public Vector3 Position
	{
		get => camera.Position;
		set => camera.Position = value;
	}

	private Raylib_cs.Camera3D camera;

	// TODO: Define this in the parent maybe
	internal Raylib_cs.Camera3D AsRaylibVersion => camera;

	protected internal override void Start()
	{
		camera = new Raylib_cs.Camera3D();
		camera.Projection = CameraProjection.Perspective;
		camera.Up = Vector3.UnitY;
	}
}

public class Camera2D : Camera
{
	private Raylib_cs.Camera2D camera;

	// TODO: Define this in the parent maybe
	internal Raylib_cs.Camera2D AsRaylibVersion => camera;
}