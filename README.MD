# Smoke
A simple game engine to create 3D games with C# and the power of raylib.

# How everything works
basic overview thing idk. Might make a youtube video or something explaining when its all done.

## Actual Engine/running code
The whole game is represented in a single json file. This file has project settings, and all game objects. This file can be manually written to if wanted, but it's recommended to use the actual engine (will do it all for you) All game objects have components, with the most important one being the script component.

The game technically isn't actually a program, it's a library. All of the games code is compiled into an assembly. This dll is then injected into the game *runner*, which is the actual program (window and whatnot) A script component contains the url to a type/class inside the assembly. The runner parses it from the game json file, then loads the type and runs the code.

# Component-based object oriented architecture
Not as fast as an ECS, however I think its like a million times nicer on the user to have it object oriented so its a kinda weird wannabe mix but not? idk it works

# Lore
Originally was named MarlEngine but got renamed to Smoke because MarlEngine is a stink name. Started off with an ESC, but got the downgrade to kick the Entities out erhm

# Examples
Move a ball around with the arrow keys
```cs
using Smoke;
using static Smoke.Graphics;
using static Smoke.Input;
using static Smoke.Runtime;

class PlayerMovement : RenderableComponent
{
	public Transform Transform => GameObject.Get<Transform>();
	public float Speed = 500f;

	public override void Update()
	{
		Transform.Position += GetInput(ArrowKeys) * Speed * DeltaTime;
	}

	public override void Render2D()
	{
		DrawCircle(Transform, 50, Raylib_cs.Color.DarkPurple);
	}
}
```