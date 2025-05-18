using Smoke;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.Runtime;

class Temp : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();

	public override void Update()
	{
		Transform.Position += GetInput(ArrowKeys) * 500f * DeltaTime;
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 50, Raylib_cs.Color.DarkPurple);
	}
}