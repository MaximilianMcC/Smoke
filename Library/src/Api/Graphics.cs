using System.Numerics;
using Raylib_cs;
using static Smoke.AssetManager;

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

	// Textures
	public static void DrawTexture(Texture2D texture, Transform transform) => DrawTexture(texture, transform.Position, transform.Scale, 0f, Color.White);
	public static void DrawTexture(Texture2D texture, Transform transform, float rotation, Color color) => DrawTexture(texture, transform.Position, transform.Scale, rotation, color);
	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size, float rotation, Color color)
	{
		Raylib.DrawTexturePro(
			texture,
			new Rectangle(0, 0, texture.Width, texture.Height),
			new Rectangle(position, size),
			Vector2.Zero,
			rotation,
			color
		);
	}

	//? maybe don't do this
	public static void DrawTexture(string textureKey, Transform transform) => DrawTexture(Textures[textureKey], transform.Position, transform.Scale, 0f, Color.White);
}