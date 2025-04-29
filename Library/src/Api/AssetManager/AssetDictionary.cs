using Smoke;

public class AssetDictionary<TValue> : Dictionary<string, TValue>
{
	// Debug/placeholder asset for if a requested asset doesn't exist
	// TODO: Also have a file extension one
	public TValue PlaceholderAsset;

	public AssetDictionary(TValue placeholder = default)
	{
		PlaceholderAsset = placeholder;
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
				// Use the placeholder asset
				return PlaceholderAsset;
			}

			// Use the actual asset
			return value;
		}

		// Just set it like normal
		set => base[key] = value;
	}
}
