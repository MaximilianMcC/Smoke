using System.Reflection;
using Raylib_cs;
using Smoke;

class Program
{
	public static void Main(string[] args)
	{
		// Load the smoke project from arguments
		SmokeProject.Load(args[0]);

		// Create the raylib window
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(854, 480, SmokeProject.Config.DisplayName);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.SetExitKey(KeyboardKey.Null);

		// Load the first level
		SceneManager.DeserializeAllScenes();
		SceneManager.Load(SmokeProject.Config.StartingScene);

		// Main raylib loop
		while (Raylib.WindowShouldClose() == false)
		{
			SceneManager.CurrentScene.Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			// SceneManager.CurrentScene.Render3D();
			SceneManager.CurrentScene.Render2D();
			Raylib.EndDrawing();
		}

		// Unload whatever scene we're on rn
		SceneManager.CurrentScene.Unload();

		// Close raylib
		Raylib.CloseWindow();
	}
}