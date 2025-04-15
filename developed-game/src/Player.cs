using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using Raylib_cs;
using System.Numerics;

class Player : Script
{
	private const float speed = 500f;

	public override void Start()
	{
		// Chuck the player in the middle bottom
		Transform.Position = new Vector2(
			(WindowWidth - Transform.Scale.X) / 2,
			WindowHeight - Transform.Scale.Y
		);
	}

	public override void Update()
	{
		// Get input and move the player
		float movement = GetXInput(true) * speed * DeltaTime;
		Transform.Position.X += movement;

		// If the player presses the space button then shoot
		if (KeyPressed(KeyboardKey.Space)) Shoot();
	}

	private static void Shoot()
	{
		// Spawn a bullet
		// Cartographer.Spawn("76da99fb-4fc8-44d6-a72e-0e257c43cbaa");
	}

	public override void Render()
	{
		DrawSquare(Transform, Color.White);
	}
}