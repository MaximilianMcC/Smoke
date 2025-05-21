# Transform (`Smoke.Transform`)
| Variable | Type | Description |
|---|---|---|
| Position | `Vector2` | The position of whatever you're representing. |
| Rotation | `Vector2` | The rotation of whatever you're representing. |
| Scale | `Vector2` | The position of whatever you're representing. Default value of `(1, 1)` |

## Example usage
A transform is used to represent 'something' in space. For example, the position of the player.
```cs
using static Smoke.Runtime;

class PlayerMovement : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();

	public override void Update()
	{
		const float speed = 500f;
		Transform.Position += GetInput(ArrowKeys) * speed * DeltaTime;
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 50, Raylib_cs.Color.DarkPurple);
	}
}
```
This code uses `position` to move the player around and to render them. `GetInput` returns a `Vector2` which is used to give the player a direction, `Speed` is then times with it to get how much movement we want.

The value of `500` for speed might look high, but `DeltaTime` is a tiny as number so when they're timesed together the speed is brought down to a more sensible value.

Generally, if something moves every frame, you need to times it by `DeltaTime` to make it frame independent. If you don't, the speed of the object will be faster on fast computers, and slower on slow computers. Using `DeltaTime` will make it consistent on all computers. You can access `DeltaTime` from `Smoke.Runtime`.

---
[Back to home](../Docs.md)