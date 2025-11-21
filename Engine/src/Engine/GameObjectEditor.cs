using System.Numerics;
using System.Reflection;
using ImGuiNET;
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
			// Make the game object with a name
			// TODO: Use imgui for this
			GameObject newGameObject = new GameObject();
			(bool _, string text) = TinyDialogs.InputBox(InputBoxType.Text, "Name", "Game Object Name:", "MyAwesomeGameObject");
			newGameObject.DisplayName = text;

			// If we have a game object already selected then
			// make it a child. Otherwise make it a 'root'
			if (EditorUi.SelectedGameObject == null) SceneManager.CurrentScene.RootGameObjects.Add(newGameObject);
			else EditorUi.SelectedGameObject.Children.Add(newGameObject);
		}
	}
}