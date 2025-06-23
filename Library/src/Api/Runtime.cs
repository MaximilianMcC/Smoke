using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Runtime
{
	public static bool Debug = false;
	
	public static float DeltaTime => Raylib.GetFrameTime();

	// TODO: Don't cast idk
	public static float Time => (float)Raylib.GetTime();
}