using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		// make raylib window for the actual game
		// Setup raylib
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(500, 400, "Loading title name or something idk");
		Raylib.SetExitKey(KeyboardKey.Null);

		// Set the window to be half the size of the monitor rn
		// And also put it in the centre of the screen
		// TODO: Maybe don't scope stuff like this
		{
			Raylib.SetWindowSize(
				Raylib.GetMonitorWidth(Raylib.GetCurrentMonitor()) / 2,
				Raylib.GetMonitorHeight(Raylib.GetCurrentMonitor()) / 2
			);
			Raylib.SetWindowPosition(
				(Raylib.GetMonitorWidth(Raylib.GetCurrentMonitor()) / 2) / 2,
				(Raylib.GetMonitorHeight(Raylib.GetCurrentMonitor()) / 2) / 2
			);
		}

		// Load the project and scripts
		Project.Load(args[0]);
		ScriptManager.Initialise();

		// Set the game title
		Raylib.SetWindowTitle(Project.DisplayName);

		// Main program loop
		Game.Start();
		while (Raylib.WindowShouldClose() == false)
		{
			Game.Update();

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			// TODO: Do camera stuff
			Game.Render3D();
			Game.RenderDebug3D();

			Game.Render2D();
			Game.RenderDebug2D();

			Raylib.EndDrawing();
		}
		Game.TidyUp();
		Raylib.CloseWindow();
	}
}