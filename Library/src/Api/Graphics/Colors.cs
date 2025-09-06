
using Raylib_cs;

namespace Smoke;

public class Color
{
	public byte Red;
	public byte Green;
	public byte Blue;
	public byte Alpha;
	public Raylib_cs.Color AsRaylibColor => new Raylib_cs.Color(Red, Green, Blue, Alpha);

	// Lets you use hex directly without the ctor
	public static implicit operator Color(uint hex) => new Color(hex);

	public Color(uint hex)
	{
		// Check for if we supplied an alpha
		if (hex <= 0xffffff)
		{
			// Extract the R, G, and B values
			Red = (byte)((hex >> 16) & 0xff);
			Green = (byte)((hex >> 8) & 0xff);
			Blue = (byte)((hex >> 0) & 0xff);

			// Set the alpha to be full
			Alpha = byte.MaxValue;
		}
		else
		{
			// Extract the R, G, B and A values
			Red = (byte)((hex >> 24) & 0xff);
			Green = (byte)((hex >> 16) & 0xff);
			Blue = (byte)((hex >> 8) & 0xff);
			Alpha = (byte)((hex >> 0) & 0xff);
		}

		//? Two different implimentations to get the
		//? R, G, and B values are used because in
		//? the actual 'number' everything is written
		//? backwards so the position of the bytes
		//? is different with/without alpha
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