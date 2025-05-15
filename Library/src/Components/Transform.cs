using System.Numerics;
using Newtonsoft.Json;

namespace Smoke;

[JsonObject(MemberSerialization.Fields)]
public class Transform : IFixedComponent
{
	// TODO: Make a 3D version
	public Vector2 Position;
	public Vector2 Scale;
	public Vector2 Rotation;

	public override string ToString() => $"{Position}\n{Scale}\n{Rotation}";
}