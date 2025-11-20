using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;
using Smoke;

class Engine
{
	public static void Run(string[] args)
	{
		// Load the smoke project from arguments
		SmokeProject.Load(args[0]);

		// Create the raylib window
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(854, 480, $"Smoke Editor ({SmokeProject.Config.DisplayName})");
		Raylib.SetExitKey(KeyboardKey.Null);

		// Setup imgui
		rlImGui.Setup(true);

		// Load the first scene
		// TODO: Maybe don't do this way idk
		SceneManager.DeserializeAllScenes();
		SceneManager.Load(SmokeProject.Config.StartingScene);
		
		// Main raylib loop
		while (Raylib.WindowShouldClose() == false)
		{
			// If we press ctrl+s then save
			//TODO: Add a little * on the title if its not saved
			if (Raylib.IsKeyDown(KeyboardKey.LeftControl) && (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressedRepeat(KeyboardKey.S)))
			{
				SmokeProject.Save();
				Console.WriteLine("Saved");
			}

			GameObjectEditor.Update();

			Raylib.BeginDrawing();
			rlImGui.Begin();
			Raylib.ClearBackground(Color.Magenta);

			EditorUi.DrawGameObjectList();

			rlImGui.End();
			Raylib.EndDrawing();
		}

		// Close raylib
		rlImGui.Shutdown();
		Raylib.CloseWindow();
	}
}