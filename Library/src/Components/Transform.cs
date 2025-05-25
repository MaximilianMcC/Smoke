using System.Numerics;

namespace Smoke;

public class Transform2D : Component
{
	public Vector2 Position;
	public Vector2 Scale = new Vector2(1);
	public float Rotation;
}

public class Transform3D : Component
{
	public Vector3 Position;
	public Vector3 Scale = new Vector3(1);
	public Vector3 Rotation;
}