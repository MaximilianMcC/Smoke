using System.Numerics;
using Raylib_cs;

namespace Smoke;

public class Runtime
{
	public static float DeltaTime => Raylib.GetFrameTime();
}