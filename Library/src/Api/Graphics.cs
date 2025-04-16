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

	// Squares
	public static void DrawSquare(Transform transform, Color color) => DrawSquare(transform.Position, transform.Scale, color);
	public static void DrawSquare(Vector2 position, Vector2 size, Color color)
	{
		Raylib.DrawRectanglePro(new Rectangle(position, size), Vector2.Zero, 0f, color);
	}

	// Text
	public static void DrawText(string text, Transform transform, float fontSize, Color color) => DrawText(text, transform.Position, fontSize, color);
	public static void DrawText(string text, float x, float y, float fontSize, Color color) => DrawText(text, new Vector2(x, y), fontSize, color);
	public static void DrawText(string text, Vector2 position, float fontSize, Color color)
	{
		Raylib.DrawTextPro(Raylib.GetFontDefault(), text, position, Vector2.Zero, 0f, fontSize, (fontSize / 10), color);
	}
}