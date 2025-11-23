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
			// Make the new game object
			GameObject newGameObject = new GameObject();

			// If we have a game object already selected then
			// make it a child. Otherwise make it a 'root'
			if (EditorUi.SelectedGameObject == null) SceneManager.CurrentScene.RootGameObjects.Add(newGameObject);
			else EditorUi.SelectedGameObject.Children.Add(newGameObject);

			// Select the new game object
			EditorUi.SelectedGameObject = newGameObject;
			EditorUi.RenamingGameObject = newGameObject;
		}
	}
}