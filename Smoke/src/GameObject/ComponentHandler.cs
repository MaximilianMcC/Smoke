namespace Smoke;
public partial class Component
{
	private static HashSet<Type> loadedTypes = [];

	// Make sure we load the type once only
	internal void InternalLoadType()
	{
		// Get our type
		Type type = GetType();

		// Check for if we're already in the hash set
		//? Hash set is a super fast unordered list with no duplicates
		if (loadedTypes.Contains(type) == false)
		{
			// Call our special load type method (once)
			loadedTypes.Add(type);
			LoadType();
		}
	}

	// Only run if we're in debug mode
	internal void InternalRenderDebug3D()
	{
		if (Runtime.Debug == false) return;
		RenderDebug3D();
	}

	// Only run if we're in debug mode
	internal void InternalRenderDebug2D()
	{
		if (Runtime.Debug == false) return;
		RenderDebug2D();
	}

}