
using System.Runtime.InteropServices;
using Raylib_cs;

namespace Smoke;
public static class AssetGenerator
{
	public static Sound GenerateSoundWave(float frequency, float duration)
	{
		const uint sampleRate = 44100;
		const uint amplitude = 32000;

		// Get how many samples we need (data)
		uint sampleCount = (uint)(duration * sampleRate);
		short[] samples = new short[sampleCount];

		// Generate all the samples
		for (int i = 0; i < sampleCount; i++)
		{
			// erhm idk what this is doing
			samples[i] = (short)(amplitude * MathF.Sin(2 * MathF.PI * frequency * i / sampleRate));
		}

		// Gonna put the samples in here
		Sound sound;

		// Raylib C# bindings must use a pointer to make a wave
		GCHandle GarbageHandle = GCHandle.Alloc(samples, GCHandleType.Pinned);
		unsafe
		{
			// Put all the data we just generated
			// into a wave so we can be use it 
			Wave wave = new Wave()
			{
				SampleCount = sampleCount,
				SampleRate = sampleRate,
				SampleSize = 16,
				Channels = 1,
				Data = (void*)GarbageHandle.AddrOfPinnedObject()
			};

			// Load the wave as a sound
			sound = Raylib.LoadSoundFromWave(wave);
		}

		// Delete the pointer
		GarbageHandle.Free();

		// Give back our sound
		return sound;
	}
}