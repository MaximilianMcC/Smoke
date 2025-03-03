class Game : IUpdatable
{
	public static void Start()
	{
		// Load in all the scripts
		ScriptManager.Initialise("./test");

		// Run all scripts
		foreach (KeyValuePair<string, IUpdatable> script in ScriptManager.LoadedScripts)
		{
			script.Value.Start();
		}
	}

	public static void Update()
	{
		// Run all scripts
		foreach (KeyValuePair<string, IUpdatable> script in ScriptManager.LoadedScripts)
		{
			script.Value. Update();
		}
	}

	public static void Render2D()
	{
		// Run all scripts
		foreach (KeyValuePair<string, IUpdatable> script in ScriptManager.LoadedScripts)
		{
			script.Value.Render2D();
		}
	}

	public static void Render3D()
	{
		// Run all scripts
		foreach (KeyValuePair<string, IUpdatable> script in ScriptManager.LoadedScripts)
		{
			script.Value.Render3D();
		}
	}

	public static void RenderDebug2D()
	{
		// Run all scripts
		foreach (KeyValuePair<string, IUpdatable> script in ScriptManager.LoadedScripts)
		{
			script.Value.RenderDebug2D();
		}		
	}

	public static void RenderDebug3D()
	{
		// Run all scripts
		foreach (KeyValuePair<string, IUpdatable> script in ScriptManager.LoadedScripts)
		{
			script.Value.RenderDebug3D();
		}
	}

	public static void TidyUp()
	{
		// Run all scripts
		foreach (KeyValuePair<string, IUpdatable> script in ScriptManager.LoadedScripts)
		{
			script.Value.TidyUp();
		}		
	}
}