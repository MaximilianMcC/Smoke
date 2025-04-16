using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using Raylib_cs;

class Bullet : Script
{
	private const float speed = 1500f;

	public override void Start()
	{
		Console.WriteLine("kia ora");
	}

	public override void Update()
	{
		// Move the bullet up
		Transform.Position.Y -= speed * DeltaTime;

		// If the bullet goes off screen then destroy it
		if (Transform.Position.Y + Transform.Scale.Y < 0) Eradicate();
	}

	public override void Render()
	{
		DrawSquare(Transform, Color.White);
		// DrawText(ToString(), Transform, 15f, Color.Red);
	}
}