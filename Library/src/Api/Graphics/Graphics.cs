using System.Numerics;
using Raylib_cs;
using static Smoke.AssetManager;

namespace Smoke;

// TODO: Make one single Draw method that just has a grillion overloads for everything
public partial class Graphics
{
	public static string FontKey;

	public static float WindowWidth => Raylib.GetScreenWidth();
	public static float WindowHeight => Raylib.GetScreenHeight();
	// public static Vector2 WindowSize => new Vector2(WindowWidth, WindowHeight);

	public static Vector2 WindowSize
	{
		get => new Vector2(WindowWidth, WindowHeight);
		set => Raylib.SetWindowSize((int)value.X, (int)value.Y);
	}

	public static float WindowWidthHalf => WindowWidth / 2;
	public static float WindowHeightHalf => WindowHeight / 2;
	public static Vector2 WindowSizeHalf => WindowSize / 2;

	// TODO: Have the icon in the json then make it load automatically
	public static void SetIcon(string iconPath)
	{
		// Get a random as name for the icons image
		// then load the icon to the image
		string iconKey = "ICON" + Guid.NewGuid().ToString();
		Images[iconKey] = LoadImage(iconPath);

		// Set the icon
		Raylib.SetWindowIcon(Images[iconKey]);
	}

	private static Vector2 ApplyOrigin(Vector2 position, Vector2 size, Vector2 origin)
	{
		return position - (size * origin);
	}

	// Window size stuff
	public static void SetWindowSize(Vector2 size) => SetWindowSize((int)size.X, (int)size.Y);
	public static void SetWindowSize(int width, int height)
	{
		Raylib.SetWindowSize(width, height);
	}

	// Circles
	public static void DrawCircle(Transform2D transform, float radius, Color color) => DrawCircle(transform.FullPosition, radius, color);
	public static void DrawCircle(Vector2 position, float radius, Color color)
	{
		Raylib.DrawCircleV(position, radius, color.AsRaylibColor);
	}

	// Squares
	public static void DrawSquare(Transform2D transform, Color color) => DrawSquare(transform.FullPosition, transform.Size, transform.Origin, transform.Rotation, color);
	public static void DrawSquare(Vector2 position, Vector2 size, Color color) => DrawSquare(position, size, Origin.TopLeft, 0f, color);
	public static void DrawSquare(Vector2 position, Vector2 size, Vector2 origin, float rotation, Color color)
	{
		Raylib.DrawRectanglePro(
			new Rectangle(position, size),
			size * origin,
			rotation,
			color.AsRaylibColor
		);
	}

	// Outlines of squares
	// TODO: Rotation and origin support
	public static void DrawSquareOutline(Transform2D transform, float thickness, Color color) => DrawSquareOutline(transform.FullPosition, transform.Size, transform.Origin, transform.Rotation, thickness, color);
	public static void DrawSquareOutline(Vector2 position, Vector2 size, float thickness, Color color) => DrawSquareOutline(position, size, Origin.TopLeft, 0f, thickness, color);
	public static void DrawSquareOutline(Vector2 position, Vector2 size, Vector2 origin, float rotation, float thickness, Color color)
	{
		Raylib.DrawRectangleLinesEx(
			new Rectangle(position, size),
			thickness,
			color.AsRaylibColor
		);
	}

	// Text
	// TODO: Maybe make it so transforms scale acts on font size
	public static void DrawText(string text, Transform2D transform, float fontSize, Color color) => DrawText(text, transform.FullPosition, transform.Origin, transform.Rotation, fontSize, color);
	public static void DrawText(string text, float y, Color color) => DrawText(text, new Vector2(10, y), Origin.TopLeft, 0f, 30f, color);
	public static void DrawText(string text, Vector2 position, Vector2 origin, float rotation, float fontSize, Color color)
	{
		// Apply the origin
		position = ApplyOrigin(position, MeasureText(text, fontSize), origin);

		// Draw the text
		Raylib.DrawTextPro(Fonts[FontKey], text, position, Vector2.Zero, rotation, fontSize, (10 / fontSize), color.AsRaylibColor);
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
	// TODO: Just use origin.centre or whatever
	public static void DrawTextCentered(string text, Vector2 spaceToCentreTextIn, Vector2 centreAxis, float fontSize, Color color) => DrawTextCentered(text, spaceToCentreTextIn, centreAxis, 0f, fontSize, color);
	public static void DrawTextCentered(string text, Vector2 spaceToCentreTextIn, Vector2 centreAxis, float rotation, float fontSize, Color color)
	{
		// First measure the text
		Vector2 size = MeasureText(text, fontSize);

		// Calculate position according to the axis
		Vector2 position = (spaceToCentreTextIn - (size * centreAxis)) / 2;

		// Draw the text
		DrawText(text, position, Origin.TopLeft, rotation, fontSize, color);
	}

	// Textures
	public static void DrawTexture(Texture2D texture, Transform2D transform, Color color) => DrawTexture(texture, transform.FullPosition, transform.Size, transform.Origin, transform.Rotation, color);
	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size) => DrawTexture(texture, position, size, Origin.TopLeft, 0f, Color.White);
	public static void DrawTexture(Texture2D texture, Vector2 position, Vector2 size, Vector2 origin, float rotation, Color color)
	{
		Raylib.DrawTexturePro(
			texture,
			new Rectangle(0, 0, texture.Width, texture.Height),
			new Rectangle(position, size),
			size * origin,
			rotation,
			color.AsRaylibColor
		);
	}

	// Sprites
	public static void DrawSprite(Sprite sprite, Transform2D transform, Color color) => DrawSprite(sprite, transform.FullPosition, transform.Size, transform.Origin, transform.Rotation, color);
	public static void DrawSprite(Sprite sprite, Vector2 position, Vector2 size, Vector2 origin, float rotation, Color color)
	{
		DrawTexture(sprite.Texture, position, size, origin, rotation, color);
	}
}



public struct Origin
{
	public static readonly Vector2 TopLeft = new Vector2(0f);
	public static readonly Vector2 TopCentre = new Vector2(0.5f, 0f);
	public static readonly Vector2 TopRight = new Vector2(1f, 0f);

	public static readonly Vector2 MiddleLeft = new Vector2(0, 0.5f);
	public static readonly Vector2 Centre = new Vector2(0.5f);
	public static readonly Vector2 MiddleRight = new Vector2(1, 0.5f);

	public static readonly Vector2 BottomLeft = new Vector2(0, 1f);
	public static readonly Vector2 BottomCentre = new Vector2(0.5f, 1f);
	public static readonly Vector2 BottomRight = new Vector2(1f);
}