using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.AssetManager;
using Raylib_cs;

class Bullet : Script
{
	private const float speed = 500f;

    public override void Start()
    {
        Transform.Scale *= 5;
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
		DrawTexture(Textures["bullet"], Transform);
	}
}