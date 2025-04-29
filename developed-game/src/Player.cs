using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.AssetManager;
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

		PrintEmbeddedAssets();

		// Load the player sprite
		// TODO: Make this happen automatically yk
		// Textures.Add("player", LoadTexture("player"));
		Textures.Add("player", LoadTexture("player.png"));
	}

	public override void Update()
	{
		// Get input and move the player
		float movement = GetXInput(true) * speed * DeltaTime;
		Transform.Position.X += movement;

		// If the player presses the space button then shoot
		if (KeyPressed(KeyboardKey.Space)) Shoot();
	}

	private void Shoot()
	{
		// Spawn a bullet
		Entity bullet = EntityManager.CreateFromPrefab("76da99fb-4fc8-44d6-a72e-0e257c43cbaa");
		EntityManager.GetComponent<Transform>(bullet).Position = Transform.Position + new Vector2(Transform.Scale.X / 2, 0);
		EntityManager.Spawn(bullet);
	}

	public override void Render()
	{
		DrawSquare(Transform, Color.White);


		// this "works" (draws the placeholder thing)
		Raylib.DrawTexture(Textures["player"], 0, 0, Color.White);

		// this does not work. donest draw anything
		Raylib.DrawTexture(Textures["player 2 idk"], 200, 200, Color.White);
	}

	public override void TidyUp()
	{
		// Unload the player texture
		// TODO: Make this automatic also
		Raylib.UnloadTexture(Textures["player"]);
		Textures.Remove("player");
	}
}