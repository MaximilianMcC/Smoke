using System;
using System.Numerics;
using Raylib_cs;

class Player : Script
{
	public override void Update(Entity entity)
	{
		Console.WriteLine("kia ora im the player");
		// Transform transform = EntityManager.GetComponent<Transform>(entity);
		// transform.X += 0.01f;
		// Console.WriteLine(transform.X);
	}
}