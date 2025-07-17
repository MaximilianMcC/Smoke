using System.Numerics;
using static Smoke.Input;
using static Smoke.Graphics;
using static Smoke.Presets;

namespace Smoke;

public class TextInput : RenderableComponent
{
	public List<string> Lines = [ "" ];

	public string Text => string.Join("\n", Lines);
	public string CurrentLine => Lines[mainCaret.Line];

	private List<Caret> carets = [];
	private Caret mainCaret => carets[0];

	private Selection selection;

	public float FontSize = 32;
	public Color SelectionColor = 0x162F58_80;

	public override void Start()
	{
		// Add the 'main' caret
		//! This should never be removed
		carets.Add(new Caret(this));
	}

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
				WriteAfterCaret(input[i].ToString());
			}
		}

		// Handle fancy other buttons
		if (KeyPressedAndHeld(KeyboardKey.Backspace)) DeleteBeforeCaret();
		if (KeyPressedAndHeld(KeyboardKey.Delete)) DeleteAfterCaret();
		if (KeyPressedAndHeld(KeyboardKey.Left)) carets.ForEach(caret => caret.MoveBackwards());
		if (KeyPressedAndHeld(KeyboardKey.Right)) carets.ForEach(caret => caret.MoveForwards());
		if (KeyPressedAndHeld(KeyboardKey.Home)) carets.ForEach(caret => caret.MoveToFront());
		if (KeyPressedAndHeld(KeyboardKey.End)) carets.ForEach(caret => caret.MoveToEnd());
	}

	// Draw everything idk
	public override void Render2D()
	{
		//!debug
		Vector2 position = new Vector2(50);

		// Draw the text normally ig
		DrawText(Text, position, Origin.TopLeft, 0f, FontSize, Color.White);

		DrawText("KIA ORA", 100, Color.Black);
	}

	// Append text where the cursor is (normal writing)
	private void WriteAfterCaret(string newText)
	{
		// Loop over all the carets
		// TODO: Make a caret handler class that does the looping for me
		foreach (Caret caret in carets)
		{
			// Add the text 
			Lines[caret.Line] = Lines[caret.Line].Insert(caret.Index, newText);
			caret.MoveForwards(newText.Length);
		}
	}

	// Delete before before the cursor (normal deleting)
	// TODO: Rename to backspace
	private void DeleteBeforeCaret()
	{
		// Loop over all the carets
		// TODO: Make a caret handler class that does the looping for me
		foreach (Caret caret in carets)
		{
			// Make sure we don't go out of bounds
			if (caret.Index - 1 < 0) continue;

			// lol
			caret.MoveBackwards();
			Lines[caret.Line] = Lines[caret.Line].Remove(caret.Index, 1);
		}
	}

	// Delete before after the cursor (del key)
	// TODO: Rename to delete
	private void DeleteAfterCaret()
	{
		// Loop over all the carets
		// TODO: Make a caret handler class that does the looping for me
		foreach (Caret caret in carets)
		{
			// Remove the text in front
			Lines[caret.Line] = Lines[caret.Line].Remove(caret.Index, 1);
		}
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

	private class Caret
	{
		// TODO: Don't do this
		private TextInput textInput;

		public int Index;
		public int Line;

		public Caret(TextInput parent)
		{
			textInput = parent;
		}

		// Move the caret backward
		// TODO: Animate
		public void MoveBackwards(int charactersToMove = 1)
		{
			Index -= charactersToMove;

			// Check for if we've gone too far backwards
			if (Index < 0)
			{
				// Go to the start of the line
				Index = 0;

				// If there is a line above, then go there
				if (Line != 0) Line--;
			}
		}

		// Move the caret forward
		// TODO: Animate
		public void MoveForwards(int charactersToMove = 1)
		{
			Index += charactersToMove;

			// Check for if we're at the end of the line
			if (Index > textInput.CurrentLine.Length)
			{
				// If there is a below above, then go there,
				// otherwise just don't let us leave this line
				if (Line != textInput.Lines.Count - 1)
				{
					// Go to the start of the next line
					Line++;
					Index = 0;
				}
				else
				{
					Index = textInput.CurrentLine.Length;
				}
			}
		}

		// Move to the front of a line
		// TODO: Do it dependant on content. Sentence instead of line for example
		public void MoveToFront()
		{
			Index = 0;
		}

		// Move to the back/end of a line
		// TODO: Do it dependant on content. Sentence instead of line for example
		public void MoveToEnd()
		{
			Index = textInput.CurrentLine.Length;
		}

		// TODO: Add block caret support
		public void Draw()
		{
			// string textUpUntilCaret = ContentBeforeCaret();
			// Vector2 caretPosition = position + (MeasureText(textUpUntilCaret, FontSize) + caret.VisualPosition * Vector2.UnitX);
			// DrawSquare(caretPosition, new Vector2(FontSize / 15, FontSize), Color.White);
		}
	}
}