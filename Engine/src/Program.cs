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
		// TODO: Remove this
		//! debug
		Console.Clear();

		// Check for if we wanna use it as a cli tool
		if (args.Length > 0)
		{
			// Register/make all the commands
			Command publishCommand = new Command(
				"publish", "Build and compile a project to be shipped",
				new Argument("game.json path", "Path of the game.json file", "./game.json"),
				new Argument("game.dll path", "Path of the compiled game.dll file", "./game.dll"),
				new Argument("output path", "published output directory", "D:/temp/games", true)
			);

			// Check for what command was ran
			// TODO: Don't use var
			Dictionary<string, string> ranCommand;
			if (ArgumentParser.CommandRan(publishCommand, args, out ranCommand))
			{
				Console.WriteLine(ranCommand["game.json path"]);
				Console.WriteLine(ranCommand["game.dll path"]);
				Console.WriteLine(ranCommand["output path"]);
			}
		}
	}
}