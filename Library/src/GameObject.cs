// TODO: Make everything in here getters (not setters)
public class GameObject
{
	public string DisplayName { get; set; }
	public List<IComponent> Components { get; set; }
}