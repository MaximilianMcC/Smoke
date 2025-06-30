using Raylib_cs;

namespace Smoke;

public class InputManager
{
	public static bool AlreadyCollectedCharactersThisFrame;
	private static List<char> charactersPressed = [];

	// TODO: Rename
	public static void EndFrame()
	{
		AlreadyCollectedCharactersThisFrame = false;
	}

	public static List<char> GetCharactersPressed()
	{
		// If we've already collected the
		// characters then just give them back
		if (AlreadyCollectedCharactersThisFrame) return charactersPressed;

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
		AlreadyCollectedCharactersThisFrame = true;
		return charactersPressed;
	}
}