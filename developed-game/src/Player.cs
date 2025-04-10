using System;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

class Player : Script
{
	public override void Update()
	{
		// Transform transform = EntityManager.GetComponent<Transform>(entity);
		Transform transform = GetComponent<Transform>();
		Console.WriteLine(transform.Position);
	}

	public override void Render()
	{
		// Transform transform = EntityManager.GetComponent<Transform>(entity);
		// Raylib.DrawCircle((int)transform.X, (int)transform.Y, 50f, Color.White);
	}
}