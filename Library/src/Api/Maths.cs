public class Maths
{
	public static bool InRange(int variable, int lower, int upper)
	{
		return (variable >= lower) && (variable <= upper);
	}

	public static bool InRange(float variable, float lower, float upper)
	{
		return (variable >= lower) && (variable <= upper);
	}
}