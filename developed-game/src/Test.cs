using System;
using System.Numerics;

class Test : IUpdatable
{
	public void Start()
	{
		Vector2 idk = new Vector2(10, 10);
		Vector2 maybe = new Vector2(15, 15);
		Vector2 sterling = idk + maybe;

		Console.WriteLine(sterling);
		Console.WriteLine("kia ora te ao");
	}
}