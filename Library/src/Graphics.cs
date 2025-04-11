using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Graphics
{
	public static float WindowWidth => Raylib.GetScreenWidth();
	public static float WindowHeight => Raylib.GetScreenHeight();
	public static Vector2 WindowSize => new Vector2(WindowWidth, WindowHeight);

	public static void DrawCircle(Transform position, float radius, Color color) => DrawCircle(position.Position, radius, color);
	public static void DrawCircle(Vector2 position, float radius, Color color)
	{
		Raylib.DrawCircleV(position, radius, color);
	}
}