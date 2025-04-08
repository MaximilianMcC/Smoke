public class ScriptComponent : IComponent
{
	public string ClassPath { get; set; }

	// TODO: Json ignore maybe but also never gonna be reading from this so like all goods
	public Script Script;
}

public class Script
{
	public virtual void Update() {  }
	public virtual void Render() {  }
	public virtual void TidyUp() {  }
}