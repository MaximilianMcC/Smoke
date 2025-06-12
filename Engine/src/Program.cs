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
				"Publish", "build and compile a project to be shipped",
				new Argument("game.json path", "Path of the game.json file", "./game.json"),
				new Argument("game.dll path", "Path of the compiled game.dll file", "./game.dll"),
				new Argument("output path", "published output directory", "D:/temp/games", true)
			);

			// Check for what command was ran
			if (ArgumentParser.CommandRan(publishCommand, args))
			{

			}
		}
	}
}