using System.Numerics;
using static Smoke.Input;
using static Smoke.Graphics;
using static Smoke.Presets;

namespace Smoke;

public class TextInput : RenderableComponent
{
	public List<string> Lines = [];
	public string Text => string.Join("\n", Lines);

	private List<Caret> carets = [];
	private Caret mainCaret => carets[0];

	private Selection selection;

	public float FontSize = 32;
	public Color SelectionColor = 0x162F58_80;

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
	}

	// Draw everything idk
	public override void Render2D()
	{
		//!debug
		Vector2 position = new Vector2(20);

		// Draw the text normally ig
		DrawText(Text, position, Origin.TopLeft, 0f, FontSize, Color.White);
	}

	// Append text where the cursor is (normal writing)
	private void WriteAfterCursor(string newText)
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

		public int Index;
		public int Line;

		private float animationDuration = 1f;

		public void MoveBackwards(int charactersToMove = 1)
		{
			// MoveTo(position - (charactersToMove * Vector2.UnitX));
		}

		public void MoveForwards(int charactersToMove = 1)
		{
			// MoveTo(position + (charactersToMove * Vector2.UnitX));
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