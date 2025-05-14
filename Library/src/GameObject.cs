namespace Smoke;

public class GameObject
{
	// TODO: Get rid of the 'when' & 'on' rubbish maybe
	public virtual void WhenSpawned() { }
	public virtual void OnUpdate() { }
	public virtual void TidyUp() { }
}