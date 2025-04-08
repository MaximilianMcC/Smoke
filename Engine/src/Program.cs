using System.Diagnostics;
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
			if (Raylib.IsKeyPressed(KeyboardKey.F5)) Builder.BuildAndRun();
			if (Raylib.IsKeyPressed(KeyboardKey.R) && Raylib.IsKeyDown(KeyboardKey.LeftControl)) Builder.HotReload();

			// Graphics.DrawText("f5 to run\nctrl+r to hot reload", 10, 10, 50);
			// Graphics.DrawText(Builder.Status, 10, 120, 30);

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Raylib.DrawText("this the engine", 10, 10, 120, Color.White);
			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}