using System.ComponentModel;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("RUNNING GAME RN");

		// Make raylib window for the actual game
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
		GameObjectLoader.Init();
		MapLoader.LoadMap(Project.Info.StartingMap);

		// Set the game title
		Raylib.SetWindowTitle(Project.Info.DisplayName);

		// Main program loop
		// Game.Start();
		while (Raylib.WindowShouldClose() == false)
		{
			foreach ((Entity entity, List<Script> scripts) in EntityManager.GetAllScripts())
			{
				foreach (Script script in scripts)
				{
					script.Update();
				}
			}

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			// TODO: Do camera stuff
			// Game.Render3D();
			// Game.RenderDebug3D();

			// Game.Render2D();
			// Game.RenderDebug2D();

			foreach ((Entity entity, List<Script> scripts) in EntityManager.GetAllScripts())
			{
				foreach (Script script in scripts)
				{
					script.Render();
				}
			}

			Raylib.EndDrawing();
		}
		// Game.TidyUp();
		Raylib.CloseWindow();
	}
}