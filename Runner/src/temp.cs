using Smoke;

class Temp : GameObject, ICollision
{
	public Collision Collision { get; private set; }

	public Temp()
	{
		Collision = new Collision(this);
	}

	public override void OnUpdate()
	{
		Console.WriteLine("updating rn");
	}

	public void OnCollision(GameObject collider)
	{
		Console.WriteLine("collided with");
	}
}