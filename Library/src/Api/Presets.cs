using Smoke;

namespace Smoke;

public class Presets
{
	public struct Colors
	{
		public static readonly Color Transparent = new Color(0, 0, 0, 0);
		public static readonly Color White = new Color(255, 255, 255);
		public static readonly Color Black = new Color(0, 0, 0);

		public static readonly Color PureRed = new Color(255, 0, 0);
		public static readonly Color PureGreen = new Color(0, 255, 0);
		public static readonly Color PureBlue = new Color(0, 0, 255);

		public static readonly Color Magenta = new Color(255, 0, 255);
		public static readonly Color Blue = new Color(0, 114, 178);
		public static readonly Color Orange = new Color(213, 94, 0);
		public static readonly Color Yellow = new Color(240, 228, 66);
		public static readonly Color BluishGreen = new Color(0, 158, 115);
		public static readonly Color Gold = new Color(230, 159, 0);
		public static readonly Color Red = new Color(204, 121, 167);
		public static readonly Color Grey = new Color(128, 128, 128);
	}
	
	public struct Inputs
	{
		// public static readonly InputPreset WASD = new InputPreset(KeyboardKey.A, KeyboardKey.D, KeyboardKey.W, KeyboardKey.S);
		// public static readonly InputPreset ArrowKeys = new InputPreset(KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Up, KeyboardKey.Down);
	}
}