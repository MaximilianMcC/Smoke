using System.Numerics;
using static Smoke.Input;
using static Smoke.Graphics;

namespace Smoke;
public class TextInput : RenderableComponent
{
	public string Text = "";
	public int CaretIndex = 0;

	public float FontSize = 32;

	public override void Update()
	{
		// Get actual normal typing input
		List<char> input = GetCharactersPressed();
		if (input.Count != 0)
		{
			// Loop over every character in the queue
			for (int i = 0; i < input.Count; i++)
			{
				// Add it to the text
				WriteAfterCursor(input[i].ToString());
			}
		}

		// Pasting from clipboard
		if (ShortcutDone(KeyboardKey.LeftControl, KeyboardKey.V)) WriteAfterCursor(ClipboardText);

		// Deleting
		if (KeyPressedAndHeld(KeyboardKey.Backspace)) RemoveBeforeCursor();
		if (KeyPressedAndHeld(KeyboardKey.Delete)) RemoveAfterCursor();
	}

	public override void Render2D()
	{
		//!debug
		Vector2 position = new Vector2(20);

		
		// Draw the text
		DrawText(Text, position, Origin.TopLeft, 0f, FontSize, Color.White);

		// Draw the caret
		// TODO: Animate it
		// TODO: Add block caret support
		string textUpUntilCaret = Text.Substring(0, CaretIndex);
		Vector2 caretPosition = position + (MeasureText(textUpUntilCaret, FontSize) * Vector2.UnitX);
		DrawSquare(caretPosition, new Vector2(FontSize / 15, FontSize), Color.White);
	}

	// 'Paste' text where the cursor is
	private void WriteAfterCursor(string newText)
	{
		Text = Text.Insert(CaretIndex, newText);
		CaretIndex += newText.Length;
	}

	// Remove x letters from the cursors position
	private void RemoveAfterCursor(int charactersToRemove = 1)
	{
		// Make sure we're within bounds (can't delete nothing)
		if (CaretIndex < Text.Length)
		{
			// Remove the characters after/right of the caret
			Text = Text.Remove(CaretIndex, charactersToRemove);
		}
	}
	private void RemoveBeforeCursor(int charactersToRemove = 1)
	{
		// Make sure we're within bounds (can't delete nothing)
		if (CaretIndex > 0)
		{
			// Remove the characters before/left of the caret
			Text = Text.Remove(CaretIndex - 1, charactersToRemove);
			CaretIndex -= charactersToRemove;
		}
	}


}