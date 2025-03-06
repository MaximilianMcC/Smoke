class Game : IUpdatable
{
	public static void Start()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedLogicScripts)
		{
			script.Start();
		}
	}

	public static void Update()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedLogicScripts)
		{
			script.Update();
		}
	}

	public static void Render2D()
	{
		// Run all scripts
		foreach (IRenderable script in ScriptManager.LoadedRenderableScripts)
		{
			script.Render2D();
		}
	}

	public static void Render3D()
	{
		// Run all scripts
		foreach (IRenderable script in ScriptManager.LoadedRenderableScripts)
		{
			script.Render3D();
		}
	}

	public static void RenderDebug2D()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedLogicScripts)
		{
			script.RenderDebug2D();
		}		
	}

	public static void RenderDebug3D()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedLogicScripts)
		{
			script.RenderDebug3D();
		}
	}

	public static void TidyUp()
	{
		// Run all scripts
		foreach (IUpdatable script in ScriptManager.LoadedLogicScripts)
		{
			script.TidyUp();
		}		
	}
}