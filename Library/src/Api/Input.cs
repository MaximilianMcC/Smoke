
using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Input
{
	// TODO: Don't use raylib keyboard key
	public static bool KeyHeldDown(KeyboardKey key)
	{
		return Raylib.IsKeyDown(key);
	}

	public static bool KeyPressed(KeyboardKey key)
	{
		return Raylib.IsKeyPressed(key);
	}

	// return either -1, 0, or 1 depending on how the arrow keys/wasd are pressed
	public static float GetXInput(bool alsoUseArrowKeys = false)
	{
		float input = 0;
		if ((alsoUseArrowKeys && KeyHeldDown(KeyboardKey.Left)) || KeyHeldDown(KeyboardKey.A)) input--;
		if ((alsoUseArrowKeys && KeyHeldDown(KeyboardKey.Right)) || KeyHeldDown(KeyboardKey.D)) input++;

		return input;
	}

	// return either -1, 0, or 1 depending on how the arrow keys/wasd are pressed
	public static float GetYInput(bool alsoUseArrowKeys = false)
	{
		float input = 0;
		if ((alsoUseArrowKeys && KeyHeldDown(KeyboardKey.Up)) || KeyHeldDown(KeyboardKey.W)) input--;
		if ((alsoUseArrowKeys && KeyHeldDown(KeyboardKey.Down)) || KeyHeldDown(KeyboardKey.S)) input++;

		return input;
	}

	public static Vector2 GetInput(bool alsoUseArrowKeys = false)
	{
		return new Vector2(
			GetXInput(alsoUseArrowKeys),
			GetYInput(alsoUseArrowKeys)
		);
	}

}