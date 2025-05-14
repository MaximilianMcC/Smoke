using static Smoke.Runtime;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.AssetManager;
using Smoke;

class Thing1 : Script
{
	public override void ScriptInitialization()
	{
		Textures.Add("thing1", LoadTexture("./assets/thing1.png"));
	}

	public override void Start()
	{
		// Add a collider
		Collider collider = new Collider();
		EntityManager.AddComponentToEntity(collider, Entity);
	}

	public override void Update()
	{
		Transform.Position += GetInput(ArrowKeys) * 500 * DeltaTime;
	}

	public override void Render2D()
	{
		DrawTexture(Textures["thing1"], Transform);
	}
}