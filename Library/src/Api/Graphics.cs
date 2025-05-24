using System.Numerics;
using Raylib_cs;
using static Smoke.AssetManager;

namespace Smoke;

public class Graphics
{
	public static string FontKey;

	public static float WindowWidth => Raylib.GetScreenWidth();
	public static float WindowHeight => Raylib.GetScreenHeight();
	public static Vector2 WindowSize => new Vector2(WindowWidth, WindowHeight);

	// Window size stuff
	public static void SetWindowSize(Vector2 size) => SetWindowSize((int)size.X, (int)size.Y);
	public static void SetWindowSize(int width, int height)
	{
		Raylib.SetWindowSize(width, height);
	}

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

	// Outlines of squares
	public static void DrawSquareOutline(Transform transform, float thickness, Color color) => DrawSquareOutline(transform.Position, transform.Scale, thickness, color);
	public static void DrawSquareOutline(Vector2 position, Vector2 size, float thickness, Color color)
	{
		Raylib.DrawRectangleLinesEx(new Rectangle(position, size), thickness, color);
	}

	// Text
	// TODO: Maybe make it so transforms scale acts on font size
	public static void DrawText(string text, Transform transform, float fontSize, Color color) => DrawText(text, transform.Position, fontSize, color);
	public static void DrawText(string text, float x, float y, float fontSize, Color color) => DrawText(text, new Vector2(x, y), fontSize, color);
	public static void DrawText(string text, Vector2 position, float fontSize, Color color)
	{
		Raylib.DrawTextPro(Fonts[FontKey], text, position, Vector2.Zero, 0f, fontSize, (10 / fontSize), color);
	}

	// Measuring text
	public static Vector2 MeasureText(string text, float fontSize)
	{
		return Raylib.MeasureTextEx(Fonts[FontKey], text, fontSize, (10 / fontSize));
	}

	// Textures
	public static void DrawTexture(Texture2D texture, Transform transform) => DrawTexture(texture, transform.Position, transform.Scale, 0f, Color.White);
	public static void DrawTexture(Texture2D texture, Transform transform, float rotation, Color color) => DrawTexture(texture, transform.Position, transform.Scale, rotation, color);
	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size, Color color) => DrawTexture(texture, position, size, 0f, color);
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