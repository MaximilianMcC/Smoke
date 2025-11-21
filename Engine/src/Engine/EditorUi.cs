using ImGuiNET;
using Smoke;

static class EditorUi
{
	public static GameObject SelectedGameObject = null;

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

		// If the game object is selected then
		// draw it with a background highlight
		if (SelectedGameObject == gameObject) flags |= ImGuiTreeNodeFlags.Selected;

		// Check for if we've expanded the dropdown thing, and if
		// so then create the node thingy for the game object
		bool hasChildren = gameObject.Children.Count > 0;
		bool opened = hasChildren
			? ImGui.TreeNodeEx(gameObject.DisplayName, flags)
			: ImGui.TreeNodeEx(gameObject.DisplayName, flags | ImGuiTreeNodeFlags.Leaf | ImGuiTreeNodeFlags.NoTreePushOnOpen);

		// Check for if we wanna select the game object
		// or deselect the game object
		if (ImGui.IsItemClicked()) SelectedGameObject = gameObject;
		else if (ImGui.IsMouseClicked(ImGuiMouseButton.Left)) SelectedGameObject = null;

		// Recursively draw any child nodes
		if (opened && hasChildren)
		{
			foreach (GameObject child in gameObject.Children)
			{
				DrawGameObjectItem(child);
			}

			// Move onto the next game object
			ImGui.TreePop();
		}
	}
}