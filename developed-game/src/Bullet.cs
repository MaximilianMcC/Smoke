using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using Raylib_cs;

class Bullet : Script
{
	private const float speed = 10f;

	public override void Start()
	{
		Console.WriteLine("kia ora");
	}

	public override void Update()
	{
		// Move the bullet up
		Transform.Position.Y -= speed * DeltaTime;
	}

	public override void Render()
	{
		DrawSquare(Transform, Color.White);
		DrawText(ToString(), Transform, 15f, Color.Red);
	}
}