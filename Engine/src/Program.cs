using System.Diagnostics;
using Library;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		// Setup raylib
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(500, 400, "Marl Engine");
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

		// Load/setup the project
		Project.Load(args[0]);
		Builder.GamePath = args[1];

		// Main program loop
		while (Raylib.WindowShouldClose() == false)
		{
			// TODO: Put somewhere else
			// If we press F5 then run the game
			if (Raylib.IsKeyPressed(KeyboardKey.F5)) Builder.BuildAndRun();

			Graphics.DrawText($"building rn: {Builder.CurrentlyBuilding}", 10, 50, 30);

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}