using System;
using System.Numerics;
using static Library.Graphics;

class Test : IRenderable
{
	public void Render3D()
	{
		DrawText("coming from script", 10, 10, 30);
		Console.WriteLine("erhm");
	}
}