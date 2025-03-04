using System;
using System.Numerics;

class Test : IUpdatable
{
	public void Render3D()
	{
		Library.Graphics.DrawText("coming from script", 10, 10, 30);
		Console.WriteLine("erhm");
	}
}