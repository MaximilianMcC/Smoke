using System.Numerics;

namespace Smoke;

public class Transform2D : Component
{
	public Vector2 Position;
	public Vector2 Size;
	public float Rotation;

	// The 'bounds' of the transform with the
	// origin applied to the position
	//? Mostly used for collision ig
	public Vector2 Origin;
	public Vector2 PositionMin => Position - (Size * Origin);
	public Vector2 PositionMax => PositionMin + Size;

	// TODO: Rename to 'collidesWith'
	// TODO: Use the raylib collision methods
	// TODO: Make it work with rotations
	public bool Overlaps(Transform2D thing)
	{
		// Get the 'scope' of both things
		Vector2 aMin = PositionMin;
		Vector2 aMax = PositionMax;
		Vector2 bMin = thing.PositionMin;
		Vector2 bMax = thing.PositionMax;

		// Check for overlap/collision
		return (
			aMin.X < bMax.X && aMax.X > bMin.X &&
			aMin.Y < bMax.Y && aMax.Y > bMin.Y
		);
	}
}

public class Transform3D : Component
{
	public Vector3 Position;
	public Vector3 Scale = new Vector3(1);
	public Vector3 Rotation;
}