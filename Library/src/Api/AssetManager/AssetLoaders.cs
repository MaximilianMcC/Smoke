
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
}