public class AssetDictionary<TValue> : Dictionary<string, TValue>
{
	// Debug/placeholder asset for if a requested asset doesn't exist
	public TValue DebugAsset { get; set; }

	public AssetDictionary(TValue debugAsset)
	{
		DebugAsset = debugAsset;
	}

	// Indexer override (when we try access via key in square brackets)
	public new TValue this[string key]
	{
		get
		{
			// Check for if the asset they're asking for
			// actually exists (mislick prevention)
			if (TryGetValue(key, out var value) == false)
			{
				// Use the debug asset
				Console.WriteLine("Cannot find an asset labelled '" + key + "' (using default)");
				value = DebugAsset;
			}

			// Give back their asset
			return value;
		}

		// Just set it like normal
		set => base[key] = value;
	}
}
