using System.Numerics;
using Raylib_cs;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.Runtime;

namespace Smoke;

public class Temp : Component
{
	public Transform transform;

	public override void Update()
	{
		transform.Position += GetInput(ArrowKeys) * 100f * DeltaTime;
	}

	public override void Render2D()
	{
		DrawCircle(transform, 10, Color.Red);
	}
}