using System;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

class Player : Script
{
	public override void Update(Entity entity)
	{
		Transform transform = EntityManager.GetComponent<Transform>(entity);
		Console.WriteLine(transform.Position);
	}

	public override void Render(Entity entity)
	{
		Transform transform = EntityManager.GetComponent<Transform>(entity);
		// Raylib.DrawCircle((int)transform.X, (int)transform.Y, 50f, Color.White);
	}
}