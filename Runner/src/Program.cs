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
		LoadInitialMap();

		// Set the game title
		Raylib.SetWindowTitle(Project.Info.DisplayName);

		// Main program loop
		while (Raylib.WindowShouldClose() == false)
		{
			// Toggle debug mode
			if (Input.KeyPressed(Input.ToggleDebugKey)) Runtime.Debug = !Runtime.Debug;

			// Update all game object components
			foreach (GameObject gameObject in GameObjectManager.GameObjects)
			{
				// Loop over every component and 'run' it
				foreach (IComponent component in gameObject.Components)
				{
					component.Run();
				}
			}

			// Update all game objects
			foreach (GameObject gameObject in GameObjectManager.GameObjects)
			{
				gameObject.OnUpdate();
			}

			// Draw everything
			// TODO: Do camera stuff
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);

			// Draw 3D stuff
			// TODO: Don't loop over this again
			foreach (GameObject gameObject in GameObjectManager.GameObjects)
			{
				// Loop over every render component
				foreach (Renderer renderer in gameObject.Components.OfType<Renderer>())
				{
					if (Runtime.Debug) renderer.RenderDebug3D();

					renderer.Render3D();
				}
			}

			// Draw 2D stuff
			// TODO: Don't loop over this again again
			foreach (GameObject gameObject in GameObjectManager.GameObjects)
			{
				// Loop over every render component
				foreach (Renderer renderer in gameObject.Components.OfType<Renderer>())
				{
					if (Runtime.Debug) renderer.RenderDebug2D();
					
					renderer.Render2D();
				}
			}

			Raylib.EndDrawing();
		}

		// Unload anything that we forgot to (slacker)
		AssetManager.UnloadAllAssets();

		// Close raylib
		Raylib.CloseWindow();
	}



	private static void LoadInitialMap()
	{
		// Load everything in the map
	}
}