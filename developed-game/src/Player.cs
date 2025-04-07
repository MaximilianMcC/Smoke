using System;
using System.Numerics;
using Raylib_cs;
using static Library.Graphics;

class Player : IScript
{
	public void Update(Entity entity)
	{
		Transform transform = EntityManager.GetComponent<Transform>(entity);

		// erhm
		if (Raylib.IsKeyDown(KeyboardKey.Right)) transform.Position.X += 100 * Raylib.GetFrameTime();
		if (Raylib.IsKeyDown(KeyboardKey.Left)) transform.Position.X -= 100 * Raylib.GetFrameTime();
	}
}