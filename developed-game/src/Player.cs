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

		// Load the player sprite
		// TODO: Make this happen automatically yk
		// TODO: Make a method that only runs once for each type of prefab (lets you load stuff and whatnot)
		Textures.Add("player", LoadTexture("./assets/player.png"));
		Textures.Add("bullet", LoadTexture("./assets/bullet.png"));
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
		// Make a bullet
		Entity bullet = EntityManager.CreateFromPrefab("Bullet");

		// Make it spawn in the centre of the player
		Vector2 bulletPosition = new Vector2(Transform.Scale.X / 2, 0);
		bullet.GetComponent<Transform>().Position = bulletPosition;

		// Spawn it
		EntityManager.Spawn(bullet);
	}

	public override void Render()
	{
		DrawSquare(Transform, Color.White);

		DrawTexture(Textures["player"], Transform);
	}

	public override void TidyUp()
	{
		// Unload the player texture
		// TODO: Make this automatic also
		Raylib.UnloadTexture(Textures["player"]);
		Textures.Remove("player");

		Raylib.UnloadTexture(Textures["bullet"]);
		Textures.Remove("bullet");
	}
}