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

	public static float WindowWidthHalf => WindowWidth / 2;
	public static float WindowHeightHalf => WindowHeight / 2;
	public static Vector2 WindowSizeHalf => WindowSize / 2;

	// Window size stuff
	public static void SetWindowSize(Vector2 size) => SetWindowSize((int)size.X, (int)size.Y);
	public static void SetWindowSize(int width, int height)
	{
		Raylib.SetWindowSize(width, height);
	}

	// Circles
	public static void DrawCircle(Transform2D transform, float radius, Color color) => DrawCircle(transform.Position, radius, color);
	public static void DrawCircle(Vector2 position, float radius, Color color)
	{
		Raylib.DrawCircleV(position, radius, color);
	}

	// Squares
	public static void DrawSquare(Transform2D transform, Color color) => DrawSquare(transform.Position, transform.Scale, color);
	public static void DrawSquare(Vector2 position, Vector2 size, Color color)
	{
		Raylib.DrawRectanglePro(new Rectangle(position, size), Vector2.Zero, 0f, color);
	}

	// Outlines of squares
	public static void DrawSquareOutline(Transform2D transform, float thickness, Color color) => DrawSquareOutline(transform.Position, transform.Scale, thickness, color);
	public static void DrawSquareOutline(Vector2 position, Vector2 size, float thickness, Color color)
	{
		Raylib.DrawRectangleLinesEx(new Rectangle(position, size), thickness, color);
	}

	// Text
	// TODO: Maybe make it so transforms scale acts on font size
	public static void DrawText(string text, Transform2D transform, float fontSize, Color color) => DrawText(text, transform.Position, fontSize, color);
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

	// Draw text in the centre of a region
	// TODO: Ensure the vector is either 1 or 0
	//? (1, 0) = horizontally centered
	//? (0, 1) = vertically centered
	//? (1, 1) = middle centered
	public static void DrawTextCentered(string text, Vector2 spaceToCentreTextIn, Vector2 centreAxis, float fontSize, Color color)
	{
		// First measure the text
		Vector2 size = MeasureText(text, fontSize);

		// Calculate position according to the axis
		Vector2 position = (spaceToCentreTextIn - (size * centreAxis)) / 2;

		// Draw the text
		DrawText(text, position, fontSize, color);
	}

	// Textures
	public static void DrawTexture(Texture2D texture, Transform2D transform, Color color) => DrawTexture(texture, transform.Position, transform.Scale, Origin.TopLeft, transform.Rotation, color);
	public static void DrawTexture(Texture2D texture, Transform2D transform, Vector2 origin, Color color) => DrawTexture(texture, transform.Position, transform.Scale, origin, transform.Rotation, color);
	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size) => DrawTexture(texture, position, size, Origin.TopLeft, 0f, Color.White);
	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size, Vector2 origin, float rotation, Color color)
	{
		Raylib.DrawTexturePro(
			texture,
			new Rectangle(0, 0, texture.Width, texture.Height),
			new Rectangle(position, size),
			size * origin,
			rotation,
			color
		);
	}
}

// TODO: Maybe don't do the weird tab thing
public static class Origin
{
	public static Vector2 TopLeft = new Vector2(0f);
	public static Vector2 TopCentre = new Vector2(0.5f, 0f);
	public static Vector2 TopRight = new Vector2(1f, 0f);

	public static Vector2 MiddleLeft = new Vector2(0, 0.5f);
	public static Vector2 Centre = new Vector2(0.5f);
	public static Vector2 MiddleRight = new Vector2(1, 0.5f);

	public static Vector2 BottomLeft = new Vector2(0, 1f);
	public static Vector2 BottomCentre = new Vector2(0.5f, 1f);
	public static Vector2 BottomRight = new Vector2(1f);
}