using Raylib_cs;
using Smoke;

class Engine
{
	public static void Run(string[] args)
	{
		// Load the smoke project from arguments
		SmokeProject.Load(args[0]);

		// Create the raylib window
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(854, 480, $"Smoke Editor ({SmokeProject.Config.DisplayName})");
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.SetExitKey(KeyboardKey.Null);

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
			Raylib.ClearBackground(Color.Magenta);
			Raylib.EndDrawing();
		}

		// Close raylib
		Raylib.CloseWindow();
	}
}