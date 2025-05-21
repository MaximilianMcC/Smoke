# Transform (`Smoke.Timer`)
| Variable | Type | Description |
|---|---|---|
| Duration | `float` | How many **seconds** the timer waits for |
| Repeating | `bool` | After the timer is done should it start counting again, or stop forever? (one time use or not) Default is `true`. The timer will be set to disabled once a non repeating timer is done. |
| Done | `bool` | Has `Duration` seconds elapsed since the timer started? (timer ended) |
| Enabled | `bool` | Should the timer work right now, or are you saving it for later/don't need it yet? Default is `true` |

| Method | Return Type | Description |
| `Reset` | `void` | Resets the timer. If the timer was previously disabled, it will become enabled. |


## Example usage
A timer is used to do something after `Duration` seconds.
```cs
class EnemySpawner : UpdatableComponent
{
	public Timer Timer => GameObject.Get<Timer>();

	public override void Update()
	{
		// Spawn an enemy
		if (Timer.Done) CurrentScene.CreatePrefab("enemy", "myNewEnemy");
	}
}
```
This will spawn an enemy every 5 seconds.

---
[Back to home](../Docs.md)