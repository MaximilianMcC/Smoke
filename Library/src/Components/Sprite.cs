using Raylib_cs;
using static Smoke.Runtime;
using static Smoke.AssetManager;
namespace Smoke;

public class Sprite : UpdatableComponent
{
	public Texture2D Texture { get; private set; }
	public List<string> Frames = [];
	public int FrameIndex;

	public float Fps;
	private float elapsedTime;

	public void AddFrames(params string[] textureKeys) => Frames.AddRange(textureKeys);

	public override void Update()
	{
		// Check for if we have anything to render
		if (Frames.Count == 0) return;

		// Update the frame timer
		elapsedTime += DeltaTime;

		// Check for if we need to switch frame
		if (elapsedTime > 1 / Fps)
		{
			// Reset the timer
			elapsedTime = 0;

			// Move onto the next frame
			FrameIndex++;
			FrameIndex %= Frames.Count;
			Texture = Textures[Frames[FrameIndex]];

			// 
		}
	}
}