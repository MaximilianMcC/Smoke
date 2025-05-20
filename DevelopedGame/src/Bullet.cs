using System;
using System.Numerics;
using Smoke;
using static Smoke.Graphics;
using static Smoke.Runtime;

class Bullet : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();
	public const float Speed = 600f;

	public override void Update()
	{
		// Move up
		Transform.Position += Vector2.UnitY * -Speed * DeltaTime;

		// TODO: Remove the bullet after 10 seconds
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 20, Raylib_cs.Color.DarkGreen);
	}
}