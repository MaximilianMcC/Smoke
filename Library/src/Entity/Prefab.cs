// The prefab class is NOT used. It is only a template
// to create entities from. An eternal parent kinda idk
public class Prefab
{
	public string DisplayName { get; set; }
	public string Guid { get; set; }
	public List<IComponent> Components { get; set; }
}