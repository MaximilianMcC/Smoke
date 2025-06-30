using System.Numerics;
using static Smoke.Input;
using static Smoke.Graphics;

namespace Smoke;

public class TextInput : RenderableComponent
{
	public string Text = "";
	public int CaretIndex = 0;

	public float FontSize = 32;

	private Selection selection;
	private bool somethingsSelected => selection != null;

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

		// Clipboard
		if (ShortcutDone(KeyboardKey.LeftControl, KeyboardKey.V)) WriteAfterCursor(ClipboardText);
		if (ShortcutDone(KeyboardKey.LeftControl, KeyboardKey.C))
		{
			// If we've got something selected then use that,
			// otherwise use the entire line/sentence
			ClipboardText = somethingsSelected ? GetSelectedText() : GetCurrentContent();
		}

		// Deleting
		if (KeyPressedAndHeld(KeyboardKey.Backspace)) RemoveBeforeCursor();
		if (KeyPressedAndHeld(KeyboardKey.Delete)) RemoveAfterCursor();

		// Drastic navigation
		if (KeyPressedAndHeld(KeyboardKey.Home)) GoToStartOfText();
		if (KeyPressedAndHeld(KeyboardKey.End)) GoToEndOfText();

		// General navigation
		if (KeyPressedAndHeld(KeyboardKey.Left))
		{
			Deselect();
			MoveLeft();
		}
		if (KeyPressedAndHeld(KeyboardKey.Right))
		{
			Deselect();
			MoveRight();
		}

		// Selecting
		if (ShortcutDone(KeyboardKey.LeftControl, KeyboardKey.A)) SelectAll();
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

	// Delete selected stuff
	private void DeleteSelectedStuff()
	{
		// Check for if theres anything to delete
		if (somethingsSelected == false) return;

		//! this MIGHT be reused code idk though
		Text = Text.Remove(selection.StartIndex, selection.EndIndex);
		CaretIndex -= selection.Length;
		Deselect();
	}

	// Remove x letters from the cursors position
	private void RemoveAfterCursor(int charactersToRemove = 1)
	{
		// If we've got selected stuff to
		// delete then just delete it 
		DeleteSelectedStuff();

		// Make sure we're within bounds (can't delete nothing)
		if (CaretIndex < Text.Length)
		{
			// Remove the characters after/right of the caret
			Text = Text.Remove(CaretIndex, charactersToRemove);
		}
	}
	private void RemoveBeforeCursor(int charactersToRemove = 1)
	{
		// If we've got selected stuff to
		// delete then just delete it 
		DeleteSelectedStuff();

		// Make sure we're within bounds (can't delete nothing)
		if (CaretIndex > 0)
		{
			// Remove the characters before/left of the caret
			Text = Text.Remove(CaretIndex - 1, charactersToRemove);
			CaretIndex -= charactersToRemove;
		}
	}

	// Drastically jump between text
	private void GoToStartOfText() => CaretIndex = 0;
	private void GoToEndOfText() => CaretIndex = Text.Length;

	// Jump between sentences
	private void GoToStartOfSentence() => throw new NotImplementedException();
	private void GoToEndOfSentence() => throw new NotImplementedException();

	// Move around
	private void MoveLeft()
	{
		// Check for if we have somewhere to move to
		if (!Maths.InRange(CaretIndex, 0, Text.Length)) return;
		CaretIndex--;
	}
	private void MoveRight()
	{
		// Check for if we have somewhere to move to
		if (!Maths.InRange(CaretIndex, 0, Text.Length)) return;
		CaretIndex++;
	}

	// Get selected text
	private string GetSelectedText()
	{
		// Check for if we actually have anything selected 
		// in the first place (kinda important ngl)
		if (somethingsSelected == false) return null;

		// Get the selected text
		return Text.Substring(selection.StartIndex, selection.Length);
	}

	// Get current content. This can either be the current
	// sentence, or the current line (depends on what you want)
	// or maybe even current paragraph idk bru
	private string GetCurrentContent()
	{
		//! Placeholder
		return Text;
	}

	// Selection stuff
	private void PrimeSelection()
	{
		// Make a new selection if there is not already one
		if (somethingsSelected) return;

		selection = new Selection()
		{
			StartIndex = CaretIndex,
			EndIndex = CaretIndex
		};
	}
	private void Deselect() => selection = null;
	private void SelectAll()
	{
		PrimeSelection();
		selection.StartIndex = 0;
		selection.EndIndex = Text.Length;
	}






	// TODO: Don't use a class
	private class Selection
	{
		public int StartIndex;
		public int EndIndex;

		public int Length
		{
			get
			{
				// Get the distance between the start/end positions
				int selectionStart = Math.Min(StartIndex, EndIndex);
				int selectionEnd = Math.Max(StartIndex, EndIndex);
				return selectionEnd - selectionStart;
			}
		}
	}
}