class GameObject
{
    public List<string> ScriptPaths;
    public string Name;
    public long InstanceId;

    public GameObject()
    {
        // Create a new instance id for the object
        // TODO: Rename to guid
        InstanceId = new Guid().ToString().GetHashCode();
    }
}