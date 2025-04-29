using System.Reflection;
using Raylib_cs;

namespace Smoke;

public static class AssetManager
{
	// Store all of the games resources in a dictionary
	// so we can easily get stuff via a string as a key
	public static AssetDictionary<Texture2D> Textures = new("./assets/debug.png|internal");
	//? etc...

	// TODO: Make private
	public static byte[] GetAssetBytes(string assetPath, out string extension)
	{
		// If a path ends with "|internal" then it is gotten from the libraries assets. Otherwise the calling assembly
		const string internalSuffix = "|internal";
		bool builtIn = false;
		if (assetPath.EndsWith(internalSuffix))
		{
			builtIn = true;

			// Remove the internal bit so raylib can
			// use the path for loading and stuff
			assetPath = assetPath.Replace(internalSuffix, "");
		}

		// Get the assembly and namespace
		Assembly assembly = builtIn ? typeof(AssetManager).Assembly : Assembly.GetEntryAssembly();
		string assemblyNamespace = assembly.GetName().Name.ToLower();

		// Clean and format the asset path for embedded resources
		assetPath = assetPath.TrimStart('.', '/', '\\').Replace("/", ".").Replace("\\", ".");
		string path = $"{assemblyNamespace}.{assetPath}";
		extension = Path.GetExtension(assetPath);

		Console.WriteLine("Loading asset " + path);

		// Get the stream containing the assets data
		using (Stream stream = assembly.GetManifestResourceStream(path))
		{
			// Check for if there is a stream or not
			if (stream == null)
			{
				// Complain
				Console.Error.WriteLine("😬 Could not find embedded asset at " + path);

				// Give back nothing. This will later be swapped
				// out for any placeholder assets that have been set
				return new byte[0];
			}

			// Get the stream as a byte array
			byte[] bytes;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				// Extract the bytes
				stream.CopyTo(memoryStream);

				// Give them back
				bytes = memoryStream.ToArray();
				return bytes;
			}
		}
	}

	public static Image LoadImage(string path)
	{
		// Get the asset byte array and extension
		byte[] bytes = GetAssetBytes(path, out string extension);
		if (bytes.Length == 0) bytes = Textures.PlaceholderAssetBytes;

		// Load the image from the byte array
		Image image = Raylib.LoadImageFromMemory(extension, bytes);

		// Give back the loaded image
		return image;
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

	//! Debug
	public static void PrintEmbeddedAssets()
	{
		// Get all of the assets that are embedded rn
		Assembly assembly = Assembly.GetExecutingAssembly();
		string[] assets = assembly.GetManifestResourceNames();
		
		// Print them all
		Console.WriteLine("All embedded assets:");
		foreach (string asset in assets) Console.WriteLine("- " + asset);
		Console.WriteLine();
	}
}