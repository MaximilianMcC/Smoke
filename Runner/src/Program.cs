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

		// Create the render texture to draw everything on
		// So that the window can be resized and whatnot
		RenderTexture2D output = Raylib.LoadRenderTexture((int)Graphics.WindowSize.X, (int)Graphics.WindowSize.Y);

		// Load the first level
		SceneManager.DeserializeAllScenes();
		SceneManager.Load(SmokeProject.Config.StartingScene);

		// Main raylib loop
		while (Raylib.WindowShouldClose() == false)
		{
			Scene scene = SceneManager.CurrentScene;

			// Update
			scene.Update();

			// Draw
			if (scene.ActiveCamera == null)
			{
				Raylib.DrawText("You need to chuck in a camera\nif you wanna see anything", 10, 10, 30, Color.White);
			}
			else
			{
				// Draw 3D stuff
				Raylib.BeginMode3D(scene.ActiveCamera.AsRaylibVersion);
					scene.Render3D();
					scene.DebugRender3D();
				Raylib.EndMode3D();

				// Draw 2D stuff
				// TODO: Use a camera
				scene.Render2D();
				scene.DebugRender2D();
			}

			// Draw to the screen
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Graphics.DrawTextureOverWholeScreen(output);
			Raylib.EndDrawing();
		}

		// Unload whatever scene we're on rn
		// TODO: unload type for everything?
		SceneManager.CurrentScene.Unload();

		// Unload the output render texture
		Raylib.UnloadRenderTexture(output);

		// Close raylib
		Raylib.CloseWindow();
	}
}