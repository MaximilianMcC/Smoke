using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Runtime
{
	public static bool Debug = false;
	
	public static float DeltaTime => Raylib.GetFrameTime();
}