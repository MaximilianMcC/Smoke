class Game : IUpdatable
{
	public static void Start()
	{
		// Load in all the scripts
		ScriptManager.Initialise(@"D:\code\c#\raylib\MarlEngine\developed-game\compiled\");

		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedScripts)
		{
			script.Start();
		}
	}

	public static void Update()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedScripts)
		{
			script.Update();
		}
	}

	public static void Render2D()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedScripts)
		{
			script.Render2D();
		}
	}

	public static void Render3D()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedScripts)
		{
			script.Render3D();
		}
	}

	public static void RenderDebug2D()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedScripts)
		{
			script.RenderDebug2D();
		}		
	}

	public static void RenderDebug3D()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedScripts)
		{
			script.RenderDebug3D();
		}
	}

	public static void TidyUp()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedScripts)
		{
			script.TidyUp();
		}		
	}
}