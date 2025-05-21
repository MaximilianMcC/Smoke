using System.Diagnostics;
using Raylib_cs;
using Smoke;
using static Smoke.Input;
using static Smoke.Graphics;
using static Smoke.AssetManager;
using System.Numerics;

class Program
{
	public static void Main(string[] args)
	{
		// Setup raylib
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(500, 400, "Smoke Engine");
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

		// Load the font
		Fonts["consolas"] = LoadFont("./assets/consolas.ttf");
		FontKey = "consolas";

		// Top 'info' bar
		// Bar middleBar = new Bar()

		// Left scene hierarchy/game objects & prefabs side bar
		Bar leftBar = new Bar(Vector2.UnitY, 300f, Alignment.TopLeft);

		// Right components/side bar
		Bar rightBar = new Bar(Vector2.UnitY, 300f, Alignment.TopRight);

		// Main program loop
		while (Raylib.WindowShouldClose() == false)
		{
			// TODO: Put somewhere else
			if (KeyPressed(KeyboardKey.F5)) Builder.BuildAndRun();



			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.DarkPurple);

			leftBar.Render();
			rightBar.Render();

			DrawText($"this the engine\n{Builder.Status}", 10, 10, 60, Color.White);
			Raylib.EndDrawing();
		}

		Raylib.CloseWindow();
	}
}