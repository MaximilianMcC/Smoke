using System.Numerics;
using Raylib_cs;
using static Smoke.Graphics;

class Bar
{
	public Vector2 Position;
	public Vector2 Size;

	public Bar(Vector2 orientation, float size, Alignment alignment)
	{
		// Set the size based on the orientation
		if (orientation == Vector2.UnitX) Size = new Vector2(WindowWidth, size);
		if (orientation == Vector2.UnitY) Size = new Vector2(size, WindowHeight);

		// Set the position based on the alignment
		if (alignment == Alignment.TopLeft) Position = new Vector2(0, 0);
		if (alignment == Alignment.TopRight) Position = new Vector2(WindowWidth, 0) - (Size * Vector2.UnitX);
	}

	public void Render()
	{
		DrawSquare(Position, Size, Color.DarkGray);
	}
}

enum Alignment
{
	TopLeft,
	TopRight
}