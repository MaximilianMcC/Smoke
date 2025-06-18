using System.Diagnostics;
using Raylib_cs;
using Smoke;
using static Smoke.Input;
using static Smoke.Graphics;
using static Smoke.AssetManager;
using System.Numerics;

class Program
{
	public static void Main(string[] args)
	{
		// Check for if we wanna use it as a cli tool
		if (args.Length > 0)
		{
			// Register/make all the commands
			Command publishCommand = new Command(
				"publish", "Build and compile a project to be shipped",
				new Argument("csproj", "csproj path", "Path of the games csproj file", "./game.csproj"),
				new Argument("json", "game.json path", "Path of the game.json file", "./game.json"),
				new Argument("output", "output path", "published output directory", "D:/temp/games", true)
			);

			// Check for what command was ran
			Dictionary<string, string> arguments;
			if (ArgumentParser.CommandRan(publishCommand, args, out arguments))
			{
				Builder.Package(
					arguments["csproj"],
					arguments["json"],
					arguments["output"]
				);
			}
		}
	}
}