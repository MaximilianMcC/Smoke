using Raylib_cs;

namespace Smoke;

public static class Audio
{
	// TODO: Maybe just make it so you do sound.Play() and scrap this whole class
	public static void PlaySound(Sound sound)
	{
		Raylib.PlaySound(sound);
	}

	public static void PauseSound(Sound sound)
	{
		Raylib.PauseSound(sound);
	}

	public static void StopSound(Sound sound)
	{
		Raylib.StopSound(sound);
	}
}