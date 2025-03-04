using Raylib_cs;

namespace Library;

public class Graphics
{
	public static void DrawText(string text, float x, float y, float fontSize)
	{
		Raylib.DrawText(text, (int)x, (int)y, (int)fontSize, Color.White);
	}
}