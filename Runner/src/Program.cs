using System.ComponentModel;
using Raylib_cs;

class Program
{
	public static void Main(string[] args)
	{
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
		AdvancedComponentLoader.Init();
		LoadInitialMap();

		// Set the game title
		Raylib.SetWindowTitle(Project.Info.DisplayName);

		// Main program loop
		while (Raylib.WindowShouldClose() == false)
		{

			// Update everything
			foreach (Entity entity in EntityManager.InstancedEntities)
			{
				foreach (ScriptComponent script in EntityManager.GetComponents<ScriptComponent>(entity))
				{
					script.Script.Update();
				}
			}

			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			// TODO: Do camera stuff
			// Game.Render3D();
			// Game.RenderDebug3D();

			// Game.Render2D();
			// Game.RenderDebug2D();

			// Basic render for everything
			// TODO: Split up into 2d/3d/debug
			foreach (Entity entity in EntityManager.InstancedEntities)
			{
				foreach (ScriptComponent script in EntityManager.GetComponents<ScriptComponent>(entity))
				{
					script.Script.Render();
				}
			}

			Raylib.EndDrawing();
		}
		// Game.TidyUp();
		Raylib.CloseWindow();
	}



	private static void LoadInitialMap()
	{
		// Load everything in the map
		Project.Info.CurrentMap.InstancedPrefabs.ForEach(prefab => EntityManager.CreateFromPrefabAndAdd(prefab.Guid));
	}
}