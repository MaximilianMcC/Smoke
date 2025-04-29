using Smoke;

public class AssetDictionary<TValue> : Dictionary<string, TValue>
{
	// Debug/placeholder asset for if a requested asset doesn't exist
	// TODO: Also have a file extension one
	public byte[] PlaceholderAssetBytes { get; set; }

	public AssetDictionary(string placeholderAssetPath)
	{
		// Grab all of the bytes from the placeholder asset
		PlaceholderAssetBytes = AssetManager.GetAssetBytes(placeholderAssetPath, out _);
	}

	// Indexer override (when we try access via key in square brackets)
	// TODO: Maybe just remove this while thing
	public new TValue this[string key]
	{
		get
		{
			// Check for if the asset they're asking for
			// actually exists (mislick prevention)
			if (TryGetValue(key, out TValue value) == false)
			{
				// Use the debug asset
				Console.WriteLine("Cannot find an asset labelled '" + key + "' (using default)");
				// value = DebugAsset;
				return value;
			}

			// Give back their asset
			return value;
		}

		// Just set it like normal
		set => base[key] = value;
	}
}
