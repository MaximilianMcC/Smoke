
using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Input
{
	public static KeyboardKey ToggleDebugKey = KeyboardKey.Grave;
	public static InputPreset WASD = new InputPreset(KeyboardKey.A, KeyboardKey.D, KeyboardKey.W, KeyboardKey.S);
	public static InputPreset ArrowKeys = new InputPreset(KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Up, KeyboardKey.Down);

	// TODO: Don't use raylib keyboard key
	public static bool KeyHeldDown(KeyboardKey key)
	{
		return Raylib.IsKeyDown(key);
	}

	public static bool KeyPressed(KeyboardKey key)
	{
		return Raylib.IsKeyPressed(key);
	}

	public static float GetInput(KeyboardKey negativeOutput, KeyboardKey positiveOutput)
	{
		float input = 0;
		if (KeyHeldDown(negativeOutput)) input--;
		if (KeyHeldDown(positiveOutput)) input++;

		return input;
	}

	public static Vector2 GetInput(InputPreset inputPreset) => GetInput(inputPreset.NegativeX, inputPreset.PositiveX, inputPreset.NegativeY, inputPreset.PositiveY);
	public static Vector2 GetInput(KeyboardKey negativeXOutput, KeyboardKey positiveXOutput, KeyboardKey negativeYOutput, KeyboardKey positiveYOutput)
	{
		// Create the input vector
		Vector2 input = new Vector2(
			GetInput(negativeXOutput, positiveXOutput),
			GetInput(negativeYOutput, positiveYOutput)
		);

		// Return the normalised input
		return input == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(input);
	}

	public static Vector2 MousePosition() => Raylib.GetMousePosition();
	public static bool MouseClicked(MouseButton mouseButton) => Raylib.IsMouseButtonPressed(mouseButton);
	public static bool MouseHeldDown(MouseButton mouseButton) => Raylib.IsMouseButtonDown(mouseButton);
}

public struct InputPreset
{
	public KeyboardKey NegativeX;
	public KeyboardKey PositiveX;

	public KeyboardKey NegativeY;
	public KeyboardKey PositiveY;

	public InputPreset(KeyboardKey negativeXKey, KeyboardKey positiveXKey, KeyboardKey negativeYKey, KeyboardKey positiveYKey)
	{
		NegativeX = negativeXKey;
		PositiveX = positiveXKey;

		NegativeY = negativeYKey;
		PositiveY = positiveYKey;
	}
}