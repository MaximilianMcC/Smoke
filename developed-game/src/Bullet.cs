using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using Raylib_cs;

class Bullet : Script
{
	private const float speed = 500f;

	public override void Update()
	{
		// Move the bullet up
		Transform.Position.Y -= speed * DeltaTime;
	}

	public override void Render()
	{
		DrawSquare(Transform, Color.White);
	}
}