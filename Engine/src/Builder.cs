class Builder
{
	public static void Build()
	{
		// Find all scripts in the project
		string[] scriptPaths = Directory.GetFiles(Project.Path, "*.cs", SearchOption.AllDirectories);
		foreach (string scriptPath in scriptPaths)
		{
			Compile(scriptPath);
		}
	}

	public static void Compile(string scriptPath)
	{
		// TODO: Don't hardcode
		string libraryPath = @"D:\code\c#\raylib\MarlEngine\Library\bin\Debug\net8.0\Library.dll";
		
		/*
		csc -target:library -out:bin\Test.dll -r:"D:\code\c#\raylib\MarlEngine\Library\bin\Debug\net8.0\Library.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Private.CoreLib.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Runtime.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Console.dll" -r:"C:\Program Files\dotnet\shared\Microsoft.NETCore.App\8.0.13\System.Linq.dll" .\src\Test.cs
		*/
	}
}