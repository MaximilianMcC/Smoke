using ImGuiNET;
using Smoke;

static class EditorUi
{
	public static GameObject SelectedGameObject = null;
	public static GameObject RenamingGameObject = null;
	private static string renamingBuffer = "";

	public static void DrawGameObjectList()
	{
		// Loop over all game objects and draw them
		ImGui.Begin("GameObjectList");
		foreach (GameObject gameObject in SceneManager.CurrentScene.RootGameObjects)
		{
			DrawGameObjectItem(gameObject);
		}
    	ImGui.End();
	}

	private static void DrawGameObjectItem(GameObject gameObject)
	{
		// Settings
		ImGuiTreeNodeFlags flags = ImGuiTreeNodeFlags.OpenOnArrow | ImGuiTreeNodeFlags.SpanAvailWidth;
		if (SelectedGameObject == gameObject) flags |= ImGuiTreeNodeFlags.Selected;

		// See how we need to display the thing
		bool hasChildren = gameObject.Children.Count > 0;
		bool renaming = RenamingGameObject == gameObject;

		if (!renaming)
		{
			// Draw normal tree node
			bool opened = hasChildren
				? ImGui.TreeNodeEx(gameObject.DisplayName, flags)
				: ImGui.TreeNodeEx(gameObject.DisplayName, flags | ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.NoTreePushOnOpen);

			// Selection
			if (ImGui.IsItemClicked()) SelectedGameObject = gameObject;
			else if (ImGui.IsMouseClicked(ImGuiMouseButton.Left)) SelectedGameObject = null;

			// Start renaming
			if (ImGui.IsKeyPressed(ImGuiKey.F2) && SelectedGameObject == gameObject)
			{
				RenamingGameObject = gameObject;
				renamingBuffer = gameObject.DisplayName;
				ImGui.SetKeyboardFocusHere();
			}

			// Recursively draw children
			if (opened && hasChildren)
			{
				foreach (var child in gameObject.Children)
					DrawGameObjectItem(child);

				ImGui.TreePop();
			}
		}
		else
		{
			// Draw inline text input for renaming
			ImGui.SetNextItemWidth(-1);
			if (ImGui.InputText("##rename", ref renamingBuffer, 256, ImGuiInputTextFlags.EnterReturnsTrue))
			{
				// Apply the new name
				gameObject.DisplayName = renamingBuffer;

				// Clear the buffer
				RenamingGameObject = null;
				renamingBuffer = "";
			}

			// Cancel the rename
			if (ImGui.IsKeyPressed(ImGuiKey.Escape))
			{
				RenamingGameObject = null;
				renamingBuffer = "";
			}
		}

		// If we press delete on the selected
		// object then delete the game object
		if ((gameObject == SelectedGameObject) && ImGui.IsKeyPressed(ImGuiKey.Delete))
		{
			
		}
	}

}