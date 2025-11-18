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

		// Main raylib loop
		while (Raylib.WindowShouldClose() == false)
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			Raylib.EndDrawing();
		}

		// Close raylib
		Raylib.CloseWindow();
	}
}