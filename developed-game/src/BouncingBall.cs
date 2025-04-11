using System.Numerics;
using Raylib_cs;
using static Smoke.Runtime;
using static Smoke.Graphics;
using Smoke;

class BouncingBall : Script
{
	private Vector2 direction = Vector2.One;
	private const float speed = 500f;

	public override void Update()
	{
		// Move the ball
		Transform.Position += direction * speed * DeltaTime;

		// If we hit a wall then flip its direction
		if (Transform.Position.X > WindowWidth || Transform.Position.X < 0) direction.X *= -1;
		if (Transform.Position.Y > WindowHeight || Transform.Position.Y < 0) direction.Y *= -1;
	}

	public override void Render()
	{
		DrawCircle(Transform, 10, Color.Red);
	}
}