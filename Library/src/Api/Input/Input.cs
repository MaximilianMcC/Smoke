
using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Input
{
	public static KeyboardKey ToggleDebugKey = KeyboardKey.Grave;

	// Keyboard input
	public static bool KeyHeldDown(KeyboardKey key) => Raylib.IsKeyDown((Raylib_cs.KeyboardKey)key);
	public static bool KeyPressed(KeyboardKey key) => Raylib.IsKeyPressed((Raylib_cs.KeyboardKey)key);
	public static bool KeyPressedAndHeld(KeyboardKey key) => KeyPressed(key) || Raylib.IsKeyPressedRepeat((Raylib_cs.KeyboardKey)key);
	public static bool ShortcutDone(KeyboardKey modifier, KeyboardKey key) => KeyHeldDown(modifier) && KeyPressedAndHeld(key);

	// Keyboard typing input
	public static List<char> GetCharactersPressed() => InputManager.GetCharactersPressed();
	public static string ClipboardText
	{
		get => Raylib.GetClipboardText_();
		set => Raylib.SetClipboardText(value);
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

	// TODO: Use properties with getters/setters
	//? public static Vector2 MousePosition { get => Raylib.GetMousePosition(); }
	public static Vector2 MousePosition() => Raylib.GetMousePosition();
	public static bool MouseClicked(MouseButton mouseButton) => Raylib.IsMouseButtonPressed((Raylib_cs.MouseButton)mouseButton);
	public static bool MouseHeldDown(MouseButton mouseButton) => Raylib.IsMouseButtonDown((Raylib_cs.MouseButton)mouseButton);
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