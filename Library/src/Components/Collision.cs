namespace Smoke;

public class Collision : IComponent
{
	public bool CurrentlyColliding;
	public bool WasJustColliding;

	public void OnCollision(GameObject collidedThing) { }

	public void Run()
	{
		// Update the previous status
		WasJustColliding = CurrentlyColliding;

		// TODO: Check for collision
		// for collider in allCollidersInTheGame if collider.collids return true idk
		//! temp debug
		CurrentlyColliding = true;

		// Call the collision method once and only once
		// for the specific collision (like a 'enter')
		if (CurrentlyColliding == true && WasJustColliding == false)
		{
			// TODO: Actually get the thing
			GameObject thingCollidedWith = new GameObject();
			OnCollision(thingCollidedWith);
		}
	}
}