using System.Reflection;
using System.Text;
using Raylib_cs;

namespace Smoke;

public static partial class AssetManager
{
	public static Image LoadImage(string path)
	{
		// Get the asset byte array and extension. If it doesn't
		// exist then use whatever placeholder asset is loaded
		byte[] bytes = GetAssetBytes(path, out string extension);
		if (bytes.Length == 0) return Images.PlaceholderAsset;

		// Load the image from the byte array
		Image image = Raylib.LoadImageFromMemory(extension, bytes);

		// Give back the loaded image
		return image;
	}

	public static void UnloadImage(string imageKey)
	{
		// Unload the image, and remove it from the dictionary
		Raylib.UnloadImage(Images[imageKey]);
		Images.Remove(imageKey);
	}

	public static Texture2D LoadTexture(string path)
	{
		// Load the texture as an image
		// then convert it to a texture
		Image image = LoadImage(path);
		Texture2D texture = Raylib.LoadTextureFromImage(image);

		// Unload the image since we no longer need it
		Raylib.UnloadImage(image);

		// Give back the loaded texture
		return texture;
	}

	public static void UnloadTexture(string textureKey)
	{
		// Unload the image, and remove it from the dictionary
		Raylib.UnloadTexture(Textures[textureKey]);
		Textures.Remove(textureKey);
	}

	public static Font LoadFont(string fontPath)
	{
		// Get the font byte array and extension. If it doesn't
		// exist then use whatever placeholder font is loaded
		byte[] bytes = GetAssetBytes(fontPath, out string extension);
		if (bytes.Length == 0) return Fonts.PlaceholderAsset;

		// Load the font
		const int maxSize = 512;
		Font font = Raylib.LoadFontFromMemory(extension, bytes, maxSize, null, 255);

		// Make it look half decent
		Raylib.GenTextureMipmaps(ref font.Texture);
		Raylib.SetTextureFilter(font.Texture, TextureFilter.Trilinear);

		// Give back the loaded font
		return font;
	}

	public static void UnloadFont(string fontKey)
	{
		Raylib.UnloadFont(Fonts[fontKey]);
	}

	public static Sound LoadSound(string soundPath)
	{
		// Get the sound byte array and extension. If it doesn't
		// exist then use whatever placeholder sound is loaded
		byte[] bytes = GetAssetBytes(soundPath, out string extension);
		if (bytes.Length == 0) return Sounds.PlaceholderAsset;

		// load the sound's wave, then load
		// the sound from the wave
		Wave wave = Raylib.LoadWaveFromMemory(extension, bytes);
		Sound sound = Raylib.LoadSoundFromWave(wave);

		// Unload the wave since it's not needed anymore
		Raylib.UnloadWave(wave);

		// Give back the loaded sound
		return sound;
	}

	public static void UnloadSound(string soundKey)
	{
		Raylib.UnloadSound(Sounds[soundKey]);
	}

	// TODO: Don't do this assembly thing
	public static string ReadTextFile(string filePath, Assembly assembly = null)
	{
		// Get the files byte array and if it doesn't exist
		// then just use some random as debug text
		byte[] bytes = GetAssetBytes(filePath, out _, assembly);
		if (bytes.Length == 0) return "erhm (this is the default (issue loading the file))";

		// Deserialize the bytes to text
		// TODO: Maybe like add a way to use Encoding.Unicode and whatnot
		string contents = Encoding.UTF8.GetString(bytes);
		return contents;
	}
}