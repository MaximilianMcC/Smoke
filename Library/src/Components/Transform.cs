using System.Numerics;

namespace Smoke;

public class Transform : Component
{
	public Vector2 Position;
	public Vector2 Scale = new Vector2(1);
	public Vector2 Rotation;
}