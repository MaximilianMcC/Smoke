using System.Numerics;
using Smoke;

class Temp : GameObject, ICollision
{
	public Collision Collision { get; private set; }
	public Transform Transform { get; private set; }

	public Temp()
	{
		Collision = new Collision(this);
	}

	public override void OnUpdate()
	{
		// Transform.Position.X += 10;
		// Transform.Position += new Vector2(10);
		// Console.WriteLine("updating rn");
		// Console.WriteLine(typeof(Transform).AssemblyQualifiedName);
	}

	public void OnCollision(GameObject collider)
	{
		Console.WriteLine("collided with");
	}
}