using System.Numerics;
using System.Text.Json.Serialization;

public class Transform : IComponent
{
	[JsonInclude] public Vector2 Position;
	[JsonInclude] public Vector2 Scale;

	public override string ToString()
	{
		return $"Position:\t{Position}\nScale:\t{Scale}";
	}
}