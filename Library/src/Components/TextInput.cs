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
		if (KeyPressedAndHeld(KeyboardKey.Home)) carets.ForEach(caret => caret.MoveToIndex(0));
		if (KeyPressedAndHeld(KeyboardKey.End)) carets.ForEach(caret => caret.MoveToIndex(CurrentLine.Length));
	}

	// Draw everything idk
	public override void Render2D()
	{
		//!debug
		Vector2 position = new Vector2(50);

		// Draw the text normally ig
		DrawText(Text, position, Origin.TopLeft, 0f, FontSize, Color.White);
		carets.ForEach(caret => caret.Draw(position));

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

		//? Animations go from 0f-1f
		private Vector2 animationStartPosition;
		private Vector2 animationEndPosition;
		private Vector2 animationPositionPercentage;

		private float animationDuration = 0.1f;
		private bool animating = false;
		private float lerpTime = 0f;

		public Caret(TextInput parent)
		{
			textInput = parent;
		}

		// Move the caret backward
		// TODO: Animate
		public void MoveBackwards(int charactersToMove = 1)
		{
			MoveToIndex(Index - charactersToMove);

			// Check for if we've gone too far backwards
			if (Index < 0)
			{
				// Go to the start of the line
				MoveToIndex(0);

				// If there is a line above, then go there
				if (Line != 0) Line--;
			}
		}

		// Move the caret forward
		// TODO: Animate
		public void MoveForwards(int charactersToMove = 1)
		{
			MoveToIndex(Index + charactersToMove);

			// Check for if we're at the end of the line
			if (Index > textInput.CurrentLine.Length)
			{
				// If there is a below above, then go there,
				// otherwise just don't let us leave this line
				if (Line != textInput.Lines.Count - 1)
				{
					// Go to the start of the next line
					Line++;
					MoveToIndex(0);
				}
				else
				{
					Index = textInput.CurrentLine.Length;
				}
			}
		}

		public void MoveToIndex(int newIndex)
		{
			// TODO: Make these const, or don't even have them at all
			// Say what we want the animation to do
			animationStartPosition = animationPositionPercentage;
			animationEndPosition = Vector2.One;

			// Begin the animation
			animating = true;
			lerpTime = 0;

			// Actually use the index
			Index = newIndex;
		}

		// TODO: Add block caret support
		public void Draw(Vector2 textPosition)
		{
			// Get how large the caret is
			Vector2 caretSize = new Vector2(textInput.FontSize / 10, textInput.FontSize);

			// Figure out where the caret starts (needed for non monospace fonts)
			string textUpUntilCaretForLine = textInput.CurrentLine.Substring(0, Index);
			float caretX = MeasureText(textUpUntilCaretForLine, textInput.FontSize).X;

			// Build the carets initial position
			Vector2 caretPosition = new Vector2(
				caretX,
				Line * textInput.FontSize
			);

			// Animate the caret
			Lerp();

			// Actually draw it
			DrawSquare(textPosition + (caretPosition * animationPositionPercentage), caretSize, Colors.White);
		}

		private void Lerp()
		{
			// Check if we should bother lerping
			if (animating == false) return;

			// Actually lerp
			lerpTime += Runtime.DeltaTime / animationDuration;
			animationPositionPercentage = Vector2.Lerp(animationStartPosition, animationEndPosition, lerpTime);

			// Check for if we've finished lerping
			if (lerpTime >= 1f)
			{
				animating = false;
				lerpTime = 1f;
			}
		}
	}
}