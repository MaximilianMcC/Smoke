using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Runtime
{
	public static bool Debug = false;

	public static float DeltaTime => Raylib.GetFrameTime();

	// TODO: Don't cast idk
	public static float Time => (float)Raylib.GetTime();

	public static bool WindowWasJustResized => Raylib.IsWindowResized();

	// TODO: Have an update method in here to run all the kinda stuff you dont want exposed
	// TODO: Like the update method for lerper or something idk 
}