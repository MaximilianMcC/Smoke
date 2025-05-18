using Smoke;
using static Smoke.Graphics;

class PlayerMovement : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();

	public override void Update()
	{
		Console.WriteLine("kia ora te ao");
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 50, Raylib_cs.Color.Blue);
	}
}