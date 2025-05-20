using static Smoke.Runtime;
namespace Smoke;

public class Timer : UpdatableComponent
{
	public bool Repeating = true;
	public bool Enabled = true;

	public float Duration;
	private float elapsedTime;
	public bool Done { get; private set; }

	public override void Update()
	{
		// Check for if we're allowed to slack
		if (Enabled == false) return;

		// Check for if we must repeat the timer
		if (Done && Repeating) Reset();

		// Update the timer
		elapsedTime += DeltaTime;

		// Check for if the time is up
		if (elapsedTime >= Duration)
		{
			// Say we're done
			Done = true;

			// If we aren't repeating then
			// disable the timer
			Enabled = false;
		}
	}

	public void Reset()
	{
		// Turn the timer on if its not already
		Enabled = true;

		// Start counting again
		Done = false;
		elapsedTime = 0;
	}
}