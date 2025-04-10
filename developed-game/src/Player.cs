using System;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

class Player : Script
{
	float speed = 1000f;

	public override void Update(Entity entity)
	{
		Transform transform = EntityManager.GetComponent<Transform>(entity);

		if (Raylib.IsKeyDown(KeyboardKey.Left)) transform.X -= speed * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Right)) transform.X += speed * Raylib.GetFrameTime();

		if (Raylib.IsKeyDown(KeyboardKey.Up)) transform.Y -= speed * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Down)) transform.Y += speed * Raylib.GetFrameTime();
	}

	public override void Render(Entity entity)
	{
		Transform transform = EntityManager.GetComponent<Transform>(entity);
		Raylib.DrawCircle((int)transform.X, (int)transform.Y, 50f, Color.White);
	}
}