using System;
using Smoke;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.Runtime;
using static Smoke.SceneManager;

class PlayerMovement : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();
	public float Speed = 500f;

	public override void Update()
	{
		Transform.Position += GetInput(ArrowKeys) * Speed * DeltaTime;

		// If you press space then spawn a bullet above
		if (KeyPressed(Raylib_cs.KeyboardKey.Space))
		{
			GameObject bullet = CurrentScene.CreatePrefab("bullet", "erhm");
			bullet.Get<Transform>().Position = Transform.Position;
		}
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 50, Raylib_cs.Color.DarkPurple);
	}
}