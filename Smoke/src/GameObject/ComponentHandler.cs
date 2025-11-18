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
}