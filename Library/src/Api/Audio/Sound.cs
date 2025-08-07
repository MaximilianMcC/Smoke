using Raylib_cs;

namespace Smoke;

using RaylibSound = Raylib_cs.Sound;
public class Sound
{
	// The actual sound
	private RaylibSound sound;
	private NormalisedFloat volume;
	private NormalisedFloat pan;
	private float pitch;

	// Make the static methods member properties
	public bool IsPlaying => Raylib.IsSoundPlaying(sound);

	public NormalisedFloat Volume
	{
		get => volume;
		set
		{
			volume = value;
			Raylib.SetSoundVolume(sound, volume);
		}
	}

	public NormalisedFloat Pan
	{
		get => pan;
		set
		{
			pan = value;
			Raylib.SetSoundPan(sound, pan);
		}
	}

	public float Pitch
	{
		get => pitch;
		set
		{
			pitch = value;
			Raylib.SetSoundPitch(sound, pitch);
		}
	}

	// Getter
	public static implicit operator RaylibSound(Sound sound) => sound.sound;

	// Setter
	public static implicit operator Sound(RaylibSound sound) => new Sound(sound);
	public Sound(RaylibSound actualSound) => sound = actualSound;
}