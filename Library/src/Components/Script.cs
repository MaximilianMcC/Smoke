public class ScriptComponent : IComponent
{
	public string ClassPath { get; set; }

	// TODO: Json ignore maybe but also never gonna be reading from this so like all goods
	public Script Script;
}

public class Script
{
	// TODO:
	// private Entity entity;
	// then like assign this when initially parsing idk

	public virtual void Update(Entity entity) { Console.WriteLine("upfating rn!!!!!!!!!!!!!!!!!!!!!!1 ");  }
	public virtual void Render(Entity entity) {  }
	public virtual void TidyUp(Entity entity) {  }
}