using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.AssetManager;
using Smoke;
using System.Numerics;

class Thing2 : Script
{
	public override void ScriptInitialization()
	{
		Textures.Add("thing2", LoadTexture("./assets/thing2.png"));
	}

	public override void Render2D()
	{
		DrawTexture(Textures["thing2"], Transform);
	}
}