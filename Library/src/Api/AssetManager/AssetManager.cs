using System.Reflection;
using Raylib_cs;

namespace Smoke;

public static class AssetManager
{
	// Store all of the games resources in a dictionary
	// so we can easily get stuff via a string as a key
	public static AssetDictionary<Image> Images = new(LoadImage("./assets/debug.png|internal"));
	public static AssetDictionary<Texture2D> Textures = new(LoadTexture("./assets/debug.png|internal"));
	//? etc...

	private static byte[] GetAssetBytes(string assetPath, out string extension)
	{
		// If a path ends with "|internal" then it is gotten from the libraries assets. Otherwise the calling assembly
		const string internalSuffix = "|internal";
		bool builtIn = false;
		if (assetPath.EndsWith(internalSuffix))
		{
			builtIn = true;

			// Remove the internal bit so we can
			// use the path for loading and stuff
			assetPath = assetPath.Replace(internalSuffix, "");
		}

		// Clean and format the asset path for embedded resources
		assetPath = assetPath.TrimStart('.', '/', '\\').Replace("/", ".").Replace("\\", ".");
		extension = Path.GetExtension(assetPath);

		// Get the assembly we need
		Assembly assembly;
		if (builtIn) assembly = typeof(AssetManager).Assembly;
		else assembly = Assembly.Load(Project.Info.Name);

		// Get the assets from the assembly
		string[] resources = assembly.GetManifestResourceNames();
		string asset = resources.FirstOrDefault(asset => asset.EndsWith(assetPath, StringComparison.OrdinalIgnoreCase));

		if (asset == null)
		{
			// Complain
			Console.Error.WriteLine("😬 Could not find embedded asset at " + assetPath);

			// Give back nothing. This will later be swapped
			// out for any placeholder assets that have been set
			//? this array.empty thing is static (quicker (more performant)) 
			return Array.Empty<byte>();
		}

		// Get the bytes of the asset
		using Stream stream = assembly.GetManifestResourceStream(asset);
		using MemoryStream bytesStream = new MemoryStream();
		stream.CopyTo(bytesStream);

		// Give back the asset as a byte array
		return bytesStream.ToArray();
	}

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

	//! Debug
	public static void PrintEmbeddedAssets()
	{
		var assembly = Assembly.GetCallingAssembly();
		Console.WriteLine(assembly.FullName);
		string[] assets = assembly.GetManifestResourceNames();
		
		// Print them all
		Console.WriteLine("All embedded assets:");
		foreach (string asset in assets) Console.WriteLine("- " + asset);
		Console.WriteLine();
	}
}