# Component
A component is used to add logic and hold data. Components are attached to [game objects](./GameObject.md). Like 99% of stuff happens inside components. Its good to think of components like scripts.

There are three unique types of components:
| Component Type | Usage | Methods | Examples |
|:-:|---|:-:|:-:|
| `Component` | Has no 'logical capabilities'. Just used to hold data, however, it has a `LoadType()` method. This is ran ONCE for an entire Type, so basically a single time every game. This is supposed to be used for loading assets and whatnot. The components parent [`GameObject`](./GameObject.md) is also stored here. | `LoadType()` | [`Transform`](../Components/Transform.md) |
| `UpdatableComponent` | This component is used for doing 'logical' stuff. The `Start()` method is called when spawned, the `Update()` method is called once per frame, and the `TidyUp()` method is called when the object is despawned. | `Start()`, `Update()`, `TidyUp()` | [`Timer`](../Components/Timer.md) |
| `RenderableComponent` | This is the same as an updatable component, however this time it can render stuff. You can call `Render2D` for 2D rendering, and `Render3D` for 3D rendering. 2D rendering happens after/ontop of 3D rendering. Both methods also have a debug method, which is only displayed when debug mode is on. | `Start()`, `Update()`, `Render3D()`, `Render2D()`, `RenderDebug3D()`, `RenderDebug2D()`, `TidyUp()` | None rn |

## Accessing a component from a game object
Components are "grabbed" using the `Get()` method on the owning [game object](./GameObject.md). It's recommended that you define components at the top of your file, then you can use them as if they were 'normal' variables.
```cs
public Transform Transform => GameObject.Get<Transform>();

public override void Update()
{
	// Move to the right
	Transform.Position += (Vector2.UnitX * 100) * DeltaTime;
}
```
If you have a game object, then you can also access and use its components:
```cs
Timer timer = enemySpawner.Get<Timer>();
if (timer.Done) ...
```