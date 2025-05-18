using Smoke;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.Runtime;

class PlayerMovement : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();
	public float Speed = 500f;

	public override void Update()
	{
		Transform.Position += GetInput(ArrowKeys) * Speed * DeltaTime;
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 50, Raylib_cs.Color.DarkPurple);
		DrawText("develoepd gaem", Transform, 40, Raylib_cs.Color.Blue);
	}
}