using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Transform2D : Component
{
	public Transform2D Parent;

	public Vector2 Position;
	public Vector2 FullPosition
	{
		get
		{
			// If there is a parent, then add it to
			// the current position to get it 'all'
			if (Parent == null) return Position;
			return Parent.Position + Position;
		}
	}



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

	// TODO: Don't use raylib
	// public Transform2D GetOverlap(Transform2D thing)
	public Rectangle GetOverlap(Transform2D thing)
	{
		// TODO: Flip min and max
		Rectangle self = new Rectangle(PositionMin, PositionMax);
		Rectangle other = new Rectangle(thing.PositionMin, thing.PositionMax);
		return Raylib.GetCollisionRec(self, other);
	}


	public void DrawOverlap(Transform2D thing, Color color)
	{
		// Transform2D overlap = GetOverlap(thing);
		// Graphics.DrawSquare(overlap, color);
		Raylib.DrawRectangleLinesEx(GetOverlap(thing), 2f, color);
	}
}

public class Transform3D : Component
{
	public Vector3 Position;
	public Vector3 Scale = new Vector3(1);
	public Vector3 Rotation;
}