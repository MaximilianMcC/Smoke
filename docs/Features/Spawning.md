# Spawning
Spawning is done with the `CreatePrefab()` method. You can either give it a prefabs name, prefabs, guid, or an actual [prefab `gameObject`](./GameObject.md). The second parameter is the new objects display name. Once `CreatePrefab()` is called, the start method is called 

This is an example of spawning a new `"bullet"` prefab with the name `"my bullet"`.
```cs
CurrentScene.CreatePrefab("bullet", "my bullet");
```
`CreatePrefab()` will return the game object we've just spawned. You can use this to do specific things to it, for example, spawn it at `10, 10`. Note: The component must have a [`Transform`](../Components/Transform.md) component on it for this to work.
```cs
GameObject bullet = CurrentScene.CreatePrefab("bullet", "my bullet");
bullet.Get<Transform>().Position = new Vector2(10, 10);
```


# Removing
Stuff can be removed via 

---
[Back to home](../Docs.md)