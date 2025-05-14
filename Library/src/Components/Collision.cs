using Raylib_cs;

namespace Smoke;

public interface ICollision
{
	public void OnCollision(GameObject collidedWith) { }
}

public class Collision : IUpdatableComponent
{
	private readonly ICollision handler;

	public bool CurrentlyColliding;
	public bool WasJustColliding;	

	// The collision handler is the one that is extended yk
	public Collision(ICollision collisionHandler)
	{
		handler = collisionHandler;
	}

	public void Update()
	{
		// Update the previous status
		WasJustColliding = CurrentlyColliding;

		// TODO: Check for collision
		// for collider in allCollidersInTheGame if collider.collids return true idk
		//! temp debug
		if (Raylib.IsKeyPressed(KeyboardKey.G)) CurrentlyColliding = !CurrentlyColliding; 

		// Call the collision method once and only once
		// for the specific collision (like a 'enter')
		if (CurrentlyColliding == true && WasJustColliding == false)
		{
			// TODO: Actually get the thing
			// TODO: Do NOT use dynamic its pretty unsafe I think
			//! Don't use dynamic (dodgy)
			GameObject collidedWith = new GameObject();
			handler.OnCollision(collidedWith);
		}
	}
}