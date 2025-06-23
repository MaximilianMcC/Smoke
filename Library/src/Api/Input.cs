
using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Input
{
	public static KeyboardKey ToggleDebugKey = KeyboardKey.Grave;
	public static InputPreset WASD = new InputPreset(KeyboardKey.A, KeyboardKey.D, KeyboardKey.W, KeyboardKey.S);
	public static InputPreset ArrowKeys = new InputPreset(KeyboardKey.Left, KeyboardKey.Right, KeyboardKey.Up, KeyboardKey.Down);

	// TODO: Don't use raylib keyboard key
	public static bool KeyHeldDown(KeyboardKey key) => Raylib.IsKeyDown(key);
	public static bool KeyPressed(KeyboardKey key) => Raylib.IsKeyPressed(key);

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
	public static bool MouseClicked(MouseButton mouseButton) => Raylib.IsMouseButtonPressed(mouseButton);
	public static bool MouseHeldDown(MouseButton mouseButton) => Raylib.IsMouseButtonDown(mouseButton);


	private static bool alreadyCollectedCharactersThisFrame;
	private static List<char> charactersPressed = [];

	public static char GetCharacterPressed() => GetCharactersPressed()[0];
	public static List<char> GetCharactersPressed()
	{
		// If we've already collected the
		// characters then just give them back
		if (alreadyCollectedCharactersThisFrame) return charactersPressed;

		// Clear the character list because we're gonna
		// repopulate it with the updated data rn
		charactersPressed.Clear();

		// Get the first 'mandatory' character input
		int input = Raylib.GetCharPressed();
		while (input > 0)
		{
			// Check for if its a key on the keyboard (alphabet)
			const int lowestKeyboardCharacter = ' ';
			const int highestKeyboardCharacter = '}';
			if (!Maths.InRange(input, lowestKeyboardCharacter, highestKeyboardCharacter)) continue;

			// Add the character to the list
			charactersPressed.Add((char)input);

			// Get the next key in the queue if
			// it exists (> 0) we'll keep reading
			input = Raylib.GetCharPressed();
		}

		// We have now collected all the characters
		alreadyCollectedCharactersThisFrame = true;
		return charactersPressed;
	}
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