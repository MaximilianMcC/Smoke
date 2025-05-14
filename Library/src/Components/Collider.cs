using System.Numerics;
using System.Text.Json.Serialization;

public class Collider : IComponent
{
	[JsonInclude] public Vector2 Position;
	[JsonInclude] public Vector2 Scale;

	public bool Colliding()
	{
		return false;
	}

	public override string ToString()
	{
		return $"Position:\t{Position}\nScale:\t{Scale}";
	}
}