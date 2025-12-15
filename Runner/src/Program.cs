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
			Scene scene = SceneManager.CurrentScene;



			scene.Update();

			
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			// Check for if we have a camera
			if (scene.ActiveCamera == null)
			{
				Raylib.DrawText("You need to chuck in a camera\nif you wanna see anything", 10, 10, 30, Color.White);
			}
			else
			{
				// Draw 3D stuff
				if (scene.ActiveCamera is Smoke.Camera3D camera3D)
				{
					Raylib.BeginMode3D(camera3D.AsRaylibVersion);
					scene.Render3D();
					scene.DebugRender3D();
					Raylib.EndMode3D();
				}

				// Draw 2D stuff
				if (scene.ActiveCamera is Smoke.Camera2D camera2D)
				{
					Raylib.BeginMode2D(camera2D.AsRaylibVersion);
					scene.Render2D();
					scene.DebugRender2D();
					Raylib.EndMode2D();
				}
			}
			Raylib.EndDrawing();
		}

		// Unload whatever scene we're on rn
		// TODO: unload type for everything?
		SceneManager.CurrentScene.Unload();

		// Close raylib
		Raylib.CloseWindow();
	}
}