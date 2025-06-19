using System.Reflection;
using Raylib_cs;

namespace Smoke;

public static partial class AssetManager
{
	// Store all of the games resources in a dictionary
	// so we can easily get stuff via a string as a key
	// TODO: Asset dictionary has the code in AssetLoaders.cs so its all in one place then its different classes for each asset (allow more fine tuning)
	public static AssetDictionary<Image> Images = new(LoadImage("./assets/debug.png|internal"));
	public static AssetDictionary<Texture2D> Textures = new(LoadTexture("./assets/debug.png|internal"));
	public static AssetDictionary<Font> Fonts = new(Raylib.GetFontDefault());
	//? etc...

	private static byte[] GetAssetBytes(string assetPath, out string extension, Assembly assetAssembly = null)
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
		if (assetAssembly != null) assembly = assetAssembly;
		else if (builtIn) assembly = typeof(AssetManager).Assembly;
		else assembly = Assembly.Load(Project.Namespace);

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

	public static void UnloadAllAssets()
	{
		// Unload the debug assets
		Raylib.UnloadImage(Images.PlaceholderAsset);
		Raylib.UnloadTexture(Textures.PlaceholderAsset);

		// Unload everything that has
		// been Dynamically loaded
		foreach (string key in Images.Keys) UnloadImage(key);
		foreach (string key in Textures.Keys) UnloadTexture(key);
		foreach (string key in Fonts.Keys) UnloadTexture(key);
	}

	public static void PrintAllAssets(Assembly assembly = null)
	{
		// Get the assets from the specified assembly
		if (assembly == null) assembly = Assembly.GetCallingAssembly();
		string[] assets = assembly.GetManifestResourceNames();

		if (assets.Length == 0)
		{
			Console.WriteLine($"{assembly.GetName().Name} contains no assets.");
			return;
		}
		else
		{
			Console.WriteLine($"All {assets.Length} asset(s) from {assembly.GetName().Name}:");
			for (int i = 0; i < assets.Length; i++)
			{
				Console.WriteLine((i + 1) + ")\t" + assets[i]);
			}
			return;
		}
	}
}