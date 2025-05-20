using static Smoke.Runtime;
namespace Smoke;

public class Timer : UpdatableComponent
{
	public float Duration;
	public bool Repeating;

	private float elapsedTime;
	public bool Done { get; private set; }

	public override void Update()
	{
		// Check for if we need to do nothing
		// or if we need to reset the timer
		if (Done && Repeating == false) return;
		else if (Done && Repeating) Reset();

		// Update the timer
		elapsedTime += DeltaTime;

		// Check for if the time is up
		if (elapsedTime >= Duration) Done = true;
	}

	public void Reset()
	{
		// Start counting again
		Done = false;
		elapsedTime = 0;
	}
}