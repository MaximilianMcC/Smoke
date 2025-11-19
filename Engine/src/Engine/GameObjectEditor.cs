using Raylib_cs;
using Smoke;
using TinyDialogsNet;

class GameObjectEditor
{
	public static void Update()
	{
		// If we press shift+a then add a new game object
		// TODO: Maybe ctrl+n for new game object, then shift+a for new component
		if (Raylib.IsKeyDown(KeyboardKey.LeftControl) && (Raylib.IsKeyPressed(KeyboardKey.N) || Raylib.IsKeyPressedRepeat(KeyboardKey.N)))
		{
			// Make the game object and add it to the scene
			GameObject newGameObject = new GameObject();
			SceneManager.CurrentScene.GameObjects.Add(newGameObject);

			// Ask for a name
			// TODO: Use smoke ui stuff for this
			(bool _, string text) = TinyDialogs.InputBox(InputBoxType.Text, "Name", "Game Object Name:", "MyAwesomeGameObject");
			newGameObject.DisplayName = text;
		}
	}
}