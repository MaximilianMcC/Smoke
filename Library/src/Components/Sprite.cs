using Raylib_cs;
using static Smoke.Runtime;
using static Smoke.AssetManager;
namespace Smoke;

public class Sprite : UpdatableComponent
{
	public Texture2D Texture { get; private set; }
	public List<string> Frames = [];

	public int FrameIndex;
	public bool LoopAnimation = true;
	public bool JustSwitchedToNextFrame { get; private set; }
	public bool JustDidAFullLoop { get; private set; }

	public float Fps;
	private float elapsedTime;

	public void SetFrames(params string[] textureKeys)
	{
		// Make sure they're not trolling
		// TODO: Maybe don't do this
		if (textureKeys.Length == 0) return;

		// Add the frames to the frames list
		// then start at the first frame
		//? -1 since the index is increased in the method (0)
		Frames.AddRange(textureKeys);
		MoveToNextFrame(-1);
	}

	public override void Update()
	{
		// Check for if we have anything to render
		// or if its not an animation (one frame)
		if (Frames.Count <= 1) return;

		// Check for if we need to reset the state status'
		if (JustSwitchedToNextFrame) JustSwitchedToNextFrame = false;
		if (JustDidAFullLoop) JustDidAFullLoop = false;

		// Update the frame timer
		elapsedTime += DeltaTime;

		// Check for if we need to switch frame
		//? 1/fps makes it per second I think idk i js know it works
		if (elapsedTime > (1 / Fps))
		{
			// Reset the timer
			elapsedTime = 0;

			// Update the texture
			MoveToNextFrame();
		}
	}

	public void MoveToNextFrame()
	{
		// Update the index
		FrameIndex++;
		JustSwitchedToNextFrame = true;

		// Check for if we did a full loop
		if (FrameIndex >= Frames.Count)
		{
			FrameIndex = 0;
			JustDidAFullLoop = true;

			// Check for if we don't want to loop again (suicide)
			if (LoopAnimation == false)
			{
				Enabled = false;
				return;
			}
		}

		// Update the current texture
		Texture = Textures[Frames[FrameIndex]];
	}

	public void MoveToNextFrame(int index)
	{
		FrameIndex = index;
		MoveToNextFrame();
	}
}