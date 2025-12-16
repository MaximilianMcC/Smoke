using System.Numerics;
using Raylib_cs;

namespace Smoke;
public class Graphics
{
	public static Vector2 WindowSize => new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

	// Draws a texture that covers the entire screen
	public static void DrawTextureOverWholeScreen(Texture2D texture)
	{
		Raylib.DrawTexturePro(
			texture,
			new Rectangle(0, 0, texture.Dimensions),
			new Rectangle(0, 0, WindowSize),
			Vector2.Zero,
			0f,
			Color.White
		);
	}

	// Draws a render texture that covers the entire screen
	// (it flips the render texture btw)
	public static void DrawTextureOverWholeScreen(RenderTexture2D renderTexture)
	{
		Raylib.DrawTexturePro(
			renderTexture.Texture,
			new Rectangle(0, 0, renderTexture.Texture.Dimensions),
			new Rectangle(0, 0, WindowSize * new Vector2(1, -1)),
			Vector2.Zero,
			0f,
			Color.White
		);
	}
}