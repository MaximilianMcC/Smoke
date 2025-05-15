using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Smoke;

[JsonObject(MemberSerialization.Fields)]
public class GameObject
{
	// TODO: Make this readonly somehow
	public IFixedComponent[] FixedComponents;
	public IRenderableComponent[] RenderableComponents;
	public IUpdatableComponent[] UpdatableComponents;

	// TODO: Get rid of the 'when' & 'on' rubbish maybe
	public virtual void WhenSpawned() { }
	public virtual void OnUpdate() { }
	public virtual void TidyUp() { }
}


/*
	TODO: Make partial then in component just do
	public partial class GameObject
	{
		public virtual void onCOllide;
	}
*/