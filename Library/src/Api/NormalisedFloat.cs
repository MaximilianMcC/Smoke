public struct NormalisedFloat
{
	private readonly float normalisedValue;

	// Keep the number between 0 and 1
	// TODO: Make a (-1, 1) version, and a (-1, 0, 1) version
	public NormalisedFloat(float value) => normalisedValue = Math.Clamp(value, 0f, 1f);

	// Getter
	public static implicit operator float(NormalisedFloat value) => value.normalisedValue;

	// Setter/constructor
	public static implicit operator NormalisedFloat(float value) => new NormalisedFloat(value);
}