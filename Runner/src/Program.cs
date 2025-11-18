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
		SceneManager.Load(SmokeProject.Config.CurrentScene);

		// Main raylib loop
		while (Raylib.WindowShouldClose() == false)
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Raylib.DrawText(SmokeProject.Config.DisplayName, 10, 10, 30, Color.White);
			Raylib.EndDrawing();
		}

		// Close raylib
		Raylib.CloseWindow();
	}
}