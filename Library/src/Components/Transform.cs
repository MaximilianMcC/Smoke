using System.Numerics;

namespace Smoke;

public class Transform : IFixedComponent
{
	// TODO: Make a 3D version
	public Vector2 Position;
	public Vector2 Scale;
	public Vector2 Rotation;

	public override string ToString() => $"{Position}\n{Scale}\n{Rotation}";
}