
using Raylib_cs;

namespace Smoke;
public struct Color
{
	public byte Red;
	public byte Green;
	public byte Blue;
	public byte Alpha;
	public Raylib_cs.Color AsRaylibColor => new Raylib_cs.Color(Red, Green, Blue, Alpha);

	public Color(uint hex)
	{
		Raylib_cs.Color color = Raylib.GetColor(hex);
		Red = color.R;
		Green = color.G;
		Blue = color.B;
		Alpha = color.A;
	}

	public Color(byte red, byte green, byte blue, byte alpha)
	{
		Red = red;
		Green = green;
		Blue = blue;
		Alpha = alpha;
	}

	public Color(byte red, byte green, byte blue)
	{
		Red = red;
		Green = green;
		Blue = blue;
		Alpha = 255;
	}

	public override string ToString()
	{
		return $"{{R:{Red} G:{Green} B:{Blue} A:{Alpha}}}";
	}
	


	// Kinda 'budget' set of colors. More can be found in Presets.Colors
	public static readonly Color White = new Color(255, 255, 255);
	public static readonly Color Black = new Color(0, 0, 0);
	public static readonly Color Magenta = new Color(255, 0, 255);
	public static readonly Color Transparent = new Color(0, 0, 0, 0);
}
