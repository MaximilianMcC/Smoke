using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Graphics
{
	public static float WindowWidth => Raylib.GetScreenWidth();
	public static float WindowHeight => Raylib.GetScreenHeight();
	public static Vector2 WindowSize => new Vector2(WindowWidth, WindowHeight);

	// Circles
	public static void DrawCircle(Transform transform, float radius, Color color) => DrawCircle(transform.Position, radius, color);
	public static void DrawCircle(Vector2 position, float radius, Color color)
	{
		Raylib.DrawCircleV(position, radius, color);
	}

	public static void DrawSquare(Transform transform, Color color) => DrawSquare(transform.Position, transform.Scale, color);
	public static void DrawSquare(Vector2 position, Vector2 size, Color color)
	{
		Raylib.DrawRectanglePro(new Rectangle(position, size), Vector2.Zero, 0f, color);
	}
}