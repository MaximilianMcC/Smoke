using Raylib_cs;

namespace Smoke;

public static class Audio
{
	public static void PlaySound(Sound sound)
	{
		Raylib.PlaySound(sound);
	}
	
	// TODO: Make a new sound class or something. Like a wrapper that just just a float property. Maybe an extension method idk
	public static void SetSoundsVolume(Sound sound, NormalisedFloat newVolume)
	{
		Raylib.SetSoundVolume(sound, newVolume);
	}
}