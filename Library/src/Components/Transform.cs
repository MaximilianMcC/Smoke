using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Transform2D : Component
{
	public Transform2D Parent = null;

	public Vector2 Position = Vector2.Zero;
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

	public Vector2 Size = Vector2.Zero;
	public float Rotation = 0f;

	// The 'bounds' of the transform with the
	// origin applied to the position
	//? Mostly used for collision ig
	public Vector2 Origin;
	public Vector2 TopCorner => FullPosition - (Size * Origin);
	public Vector2 BottomCorner => TopCorner + Size;

	public Transform2D() { }

	public Transform2D(Transform2D parent)
	{
		// Assign the parent
		Parent = parent;
	}

	public Transform2D(Vector2 position, Transform2D parent = null)
	{
		// Assign the values
		Parent = parent;
		Position = position;
	}

	public Transform2D(Vector2 position, Vector2 size, Transform2D parent = null)
	{
		// Assign the values
		Parent = parent;
		Position = position;
		Size = size;
	}

	public Transform2D(Vector2 position, Vector2 size, float rotation, Transform2D parent = null)
	{
		// Assign the values
		Parent = parent;
		Position = position;
		Size = size;
		Rotation = rotation;
	}


	// TODO: Don't do this. use smoke one
	internal protected Rectangle AsRaylibRectangle()
	{
		return new Rectangle(
			TopCorner.X,
			TopCorner.Y,
			Size.X,
			Size.Y
		);
	}

	// TODO: Account for rotation
	public bool Contains(Vector2 point)
	{
		Vector2 topLeft = FullPosition;
		Vector2 bottomRight = FullPosition + Size;

		return
			point.X >= topLeft.X &&
			point.X <= bottomRight.X &&
			point.Y >= topLeft.Y &&
			point.Y <= bottomRight.Y;
	}


	// TODO: Rename to 'collidesWith'
	// TODO: Use the raylib collision methods
	// TODO: Make it work with rotations
	public bool Overlaps(Transform2D thing)
	{
		// Get the 'scope' of both things
		Vector2 aMin = TopCorner;
		Vector2 aMax = BottomCorner;
		Vector2 bMin = thing.TopCorner;
		Vector2 bMax = thing.BottomCorner;

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
		Rectangle self = AsRaylibRectangle();
		Rectangle other = new Rectangle(thing.TopCorner, thing.BottomCorner);
		return Raylib.GetCollisionRec(self, other);
	}


	public void DrawOverlap(Transform2D thing, Color color)
	{
		// Transform2D overlap = GetOverlap(thing);
		// Graphics.DrawSquare(overlap, color);
		Raylib.DrawRectangleLinesEx(GetOverlap(thing), 2f, color.AsRaylibColor);
	}
}

public class Transform3D : Component
{
	public Vector3 Position;
	public Vector3 Scale = new Vector3(1);
	public Vector3 Rotation;
}