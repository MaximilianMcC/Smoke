using Raylib_cs;

class Thing
{
	public long Id { get; private set; }
	public List<string> scriptPaths = [];

	public Thing()
	{
		// Give it a new unique ID
		//? Hash code turns it to a number idk
		Id = Guid.NewGuid().GetHashCode();
	}

	public void Update()
	{

	}

	//! Just doing 2D for now. Will eventually make 3D
	public void Render()
	{
		
	}
}