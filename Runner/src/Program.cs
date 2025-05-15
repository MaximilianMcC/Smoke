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

		Console.WriteLine(typeof(Smoke.Transform).AssemblyQualifiedName);
		Console.WriteLine(typeof(Temp).AssemblyQualifiedName);


		GameObjectManager.DeserializeGameObject("{\r\n  \"$type\": \"Temp, Runner, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\r\n  \"fixedComponents\": [\r\n    {\r\n      \"$type\": \"Smoke.Transform, Smoke\",\r\n      \"Position\": { \"X\": 0, \"Y\": 0 },\r\n      \"Scale\": { \"X\": 0, \"Y\": 0 },\r\n      \"Rotation\": { \"X\": 0, \"Y\": 0 }\r\n    }\r\n  ],\r\n  \"updatableComponents\": [],\r\n  \"renderableComponents\": []\r\n}\r\n");
		Console.WriteLine(GameObjectManager.GameObjects.Count);

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
				foreach (IUpdatableComponent component in gameObject.UpdatableComponents)
				{
					component.Update();
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

			foreach (GameObject gameObject in GameObjectManager.GameObjects)
			{
				// Loop over every render component
				foreach (Renderer renderer in gameObject.RenderableComponents)
				{
					// Draw 3D stuff
					if (Runtime.Debug) renderer.RenderDebug3D();
					renderer.Render3D();

					// Draw 2D stuff
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