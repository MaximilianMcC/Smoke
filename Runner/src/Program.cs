using static Smoke.Graphics;
using static Smoke.SceneManager;
using Raylib_cs;
using Smoke;
using System.Numerics;
using Newtonsoft.Json;
using System.Reflection;

class Program
{
	public static void Main(string[] args)
	{
		// Make raylib window for the actual game
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.InitWindow(512, 512, "Loading title name or something idk");
		Raylib.InitAudioDevice();
		Raylib.SetExitKey(Raylib_cs.KeyboardKey.Null);

		// Check for if we're loading from embedded or file
		if (args.Length >= 1)
		{
			// load via file
			string projectJson = File.ReadAllText(args[0]);
			Project.Load(projectJson);
		}
		else
		{
			string projectJson = AssetManager.ReadTextFile("./GameAssets/Game.json", Assembly.GetExecutingAssembly());
			Project.Load(projectJson);
		}

		// Set the game title
		Raylib.SetWindowTitle(Project.DisplayName);

		// Main program loop
		while (Raylib.WindowShouldClose() == false)
		{
			// Toggle debug mode
			if (Input.KeyPressed(Input.ToggleDebugKey)) Runtime.Debug = !Runtime.Debug;

			// Update all game object components
			for (int i = 0; i < CurrentScene.Things.Count; i++)
			{
				// Loop over all updatable components
				foreach (UpdatableComponent component in CurrentScene.Things[i].Components.OfType<UpdatableComponent>().Where(component => component.Enabled))
				{
					component.Update();
				}
			}

			// Draw everything
			// TODO: Do camera stuff
			Raylib.BeginDrawing();
			// Raylib.ClearBackground(Smoke.Color.Magenta.RaylibColor);
			Raylib.ClearBackground(Presets.Colors.Magenta.AsRaylibColor);

			// Render stuff
			for (int i = 0; i < CurrentScene.Things.Count; i++)
			{
				// Loop over all renderable components
				foreach (RenderableComponent component in CurrentScene.Things[i].Components.OfType<RenderableComponent>().Where(component => component.Enabled))
				{
					// 'Standard' render
					component.Render3D();
					component.Render2D();

					// Debug render
					if (Runtime.Debug)
					{
						component.RenderDebug3D();
						component.RenderDebug2D();
					}
				}
			}

			Raylib.EndDrawing();
			InputManager.EndFrame();
		}

		// Unload anything that we forgot to (slacker)
		AssetManager.UnloadAllAssets();

		// Close raylib
		Raylib.CloseAudioDevice();
		Raylib.CloseWindow();
	}
}