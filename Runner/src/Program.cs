using static Smoke.Graphics;
using Raylib_cs;
using Smoke;

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
			RunOnAllInstancedEntities(entity => entity.Update());

			// Toggle debug mode
			if (Input.KeyPressed(Input.ToggleDebugKey)) Runtime.Debug = !Runtime.Debug;

			// Draw everything
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			// Draw 3d stuff
			// TODO: Do camera stuff
			RunOnAllInstancedEntities(entity => {
				entity.Render3D();
				if (Runtime.Debug) entity.RenderDebug3D();
			});

			// Draw 2d stuff
			// TODO: Do camera stuff
			RunOnAllInstancedEntities(entity => {
				entity.Render2D();
				if (Runtime.Debug) entity.RenderDebug2D();
			});

			Raylib.EndDrawing();
		}

		// Unload anything that we forgot to
		AssetManager.UnloadAllAssets();

		// Close raylib
		Raylib.CloseWindow();
	}



	private static void LoadInitialMap()
	{
		// Load everything in the map
		Project.Info.CurrentMap.InstancedPrefabs.ForEach(prefab => EntityManager.CreateAndSpawnPrefab(prefab));
	}



	private static void RunOnAllInstancedEntities(Action<Script> action)
	{
		// Loop through each entity
		for (int i = 0; i < EntityManager.InstancedEntities.Count; i++)
		{
			// Loop through each script on the entity
			foreach (ScriptComponent script in EntityManager.GetComponents<ScriptComponent>(EntityManager.InstancedEntities[i]))
			{
				// Do what we wanted to do with it
				action(script.Script);
			}
		}
	}
}