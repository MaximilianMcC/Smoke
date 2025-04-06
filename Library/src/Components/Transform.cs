using System.Numerics;

class Transform : GameObjectComponent
{
	//! Might not be able to use getters/setters on vectors
	public Vector3 Position { get; set; }
	public Vector3 Scale { get; set; }
}