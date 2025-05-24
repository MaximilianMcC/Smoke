# Transform (`Smoke.Sprite`)
| Variable | Type | Description |
|---|---|---|
| Texture | `Texture2D` | The current texture the sprite displays |
| Frames | `List<string>` | The loaded texture keys to use. |
| FrameIndex | `int` | The current frame index. `0` is the first frame. |
| LoopAnimation | `bool` | Should the animation be looped? Default to true. If looping is off, the animation will 'pause' on the final frame. | 
| JustSwitchedToNextFrame | `bool` | I kinda hope this is self explanatory ngl (`true` when it moves to the next animation frame) |
| JustDidAFullLoop | `bool` | `true` when the animation goes back to the first frame |
| Fps | `float` | How many frames per second the animation should be played at |

## Example usage
A sprite is a more "controlled" way to render textures (also supports animation (kinda important for a game ngl)) Even though there is animation support, you do not need to make a sprite animated. Frames are assigned via the `SetFrames` method. This takes in a string array, where `[0]` is the first frame, `[1]` is the second, etc
```cs
public Sprite Sprite => GameObject.Get<Sprite>();

public override void LoadType()
{
	// Load in all the textures we want to use
	Textures["frame0"] = LoadTexture("./assets/frame1.png");
	Textures["frame1"] = LoadTexture("./assets/frame2.png");
	Textures["frame2"] = LoadTexture("./assets/frame3.png");
}

public override void Start()
{
	// Say what frames we want (and in what order)
	Sprite.SetFrames("frame0", "frame1", "frame2");
}

public override void Render2D()
{
	// Draw the texture
	DrawTexture(Sprite.Texture, new Vector2(100), new Vector2(100), 0f, Raylib_cs.Color.White);
}
```

---
[Back to home](../Docs.md)