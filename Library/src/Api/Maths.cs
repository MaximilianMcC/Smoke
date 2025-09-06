using System.Numerics;

namespace Smoke;

public static class Maths
{
	public static bool InRange(int variable, int lower, int upper)
	{
		return (variable >= lower) && (variable <= upper);
	}

	public static bool InRange(float variable, float lower, float upper)
	{
		return (variable >= lower) && (variable <= upper);
	}

	public static Vector2 IndexToCoordinates(int index, int width)
	{
		return new Vector2(
			index % width,
			index / width
		);
	}

	public static int CoordinatesToInddex(Vector2 coordinates, int width)
	{
		return (int)((coordinates.Y * width) + coordinates.X);
	}
}