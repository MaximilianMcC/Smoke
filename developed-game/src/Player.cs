using System;
using System.Diagnostics;
using System.Numerics;
using Raylib_cs;

class Player : Script
{
	public override void Update()
	{
		Transform.Position.X += 100 * Raylib.GetFrameTime();
	}

	public override void Render()
	{
		Raylib.DrawCircleV(Transform.Position, 50f, Color.White);
	}
}
