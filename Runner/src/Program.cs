using System.Reflection;
using Raylib_cs;
using Smoke;

class Program
{
	public static void Main(string[] args)
	{
		// Load the smoke project from arguments
		SmokeProject.Instance.Load(args[0]);

		// Create the raylib window
		Raylib.SetTraceLogLevel(TraceLogLevel.Warning);
		Raylib.InitWindow(854, 480, SmokeProject.Instance.Settings.DisplayName);
		Raylib.SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.AlwaysRunWindow);
		Raylib.SetExitKey(KeyboardKey.Null);

		// Main raylib loop
		while (Raylib.WindowShouldClose() == false)
		{
			Raylib.BeginDrawing();
			Raylib.ClearBackground(Color.Magenta);
			Raylib.DrawText(SmokeProject.Instance.Settings.DisplayName, 10, 10, 30, Color.White);
			Raylib.EndDrawing();
		}

		// Close raylib
		Raylib.CloseWindow();
	}
}