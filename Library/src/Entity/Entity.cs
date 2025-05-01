public class Entity
{
	public Guid guid;
	public string name;

    public T GetComponent<T>() where T : IComponent
	{
		return EntityManager.GetComponent<T>(this);
	}

	public override string ToString() => $"{name} ({guid})";
}