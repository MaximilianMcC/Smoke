static class ArgumentParser
{
	public static List<Command> AllCommands = [];
	public static Command HelpCommand = new Command("help", "Shows all commands");

	public static bool CommandRan(Command command, string[] args, out Dictionary<string, string> output)
	{
		// Extract the actual command
		string ranCommandName = args[0].ToLower().Trim();

		// Check for if they ran the help command
		if (ranCommandName == HelpCommand.Name)
		{
			// Show the help command
			PrintHelp();

			output = null;
			return false;
		}

		// Check for if they supplied the correct name
		// (actually ran the command we're after)
		if (command.Name != ranCommandName)
		{
			// Check for if there are ANY commands with the supplied name
			if (AllCommands.Where(command => command.Name == ranCommandName).Count() == 0)
			{
				Console.WriteLine($"The command '{ranCommandName}' wasn't found (check spelling idk)");
				Console.WriteLine($"You can also type 'help' for a list of commands");
			}

			output = null;
			return false;
		}

		// Check for if the correct amount of arguments are present
		if ((args.Length < command.MinLength) || (args.Length > command.MaxLength))
		{
			// TODO: Add colors and icons and stuff
			Console.Error.WriteLine("Invalid argument count!");
			Console.Error.WriteLine($"Type 'smoke {command.Name} help' for advanced help");
			Console.WriteLine();
			command.PrintUsage();
		}

		// return true;
		output = null;
		return false;
	}

	public static void PrintHelp()
	{
		Console.WriteLine("Full command list:");
		foreach (Command command in AllCommands)
		{
			Console.WriteLine($"{command.Name}\t\t{command.Description}");
		}
		Console.WriteLine();
		Console.WriteLine("For help on a specific command, type '<command name> help'");
		Console.WriteLine($"example: '{AllCommands[1].Name} help'");
	}
}

// TODO: Make it so you can add aliases to commands (multiple names)
class Command
{
	public string Name;
	public string Description;
	public Argument[] Arguments;

	public Command(string name, string description, params Argument[] arguments)
	{
		Name = name;
		Description = description;
		Arguments = arguments;

		// Automatically add the command to the
		// full command list so the help command
		// and whatnot is automatically generated
		ArgumentParser.AllCommands.Add(this);
	}

	//? 1 is added for the command name
	public int MaxLength => 1 + Arguments.Length;
	public int MinLength => 1 + Arguments.Where(arg => arg.Optional).Count();

	public void PrintUsage()
	{
		// TODO: Add colors and icons and stuff
		Console.WriteLine("Example usage:");

		// Show the command template/syntax
		Console.Write($"{Name} ");
		foreach (Argument arg in Arguments) Console.Write($"{arg.GetValue()} ");
		Console.WriteLine();

		// Show an example (including any optional parameters)
		Console.Write($"{Name} ");
		foreach (Argument arg in Arguments) Console.Write($"{arg.ExampleValue} ");
		Console.WriteLine();

		// Show an example (including only required parameters)
		if (Arguments.Where(arg => arg.Optional == false).Count() > 1)
		{
			Console.Write($"{Name} ");
			foreach (Argument arg in Arguments.Where(arg => arg.Optional == false)) Console.Write($"{arg.ExampleValue} ");
			Console.WriteLine();
		}
	}

	public void PrintFullUsage()
	{
		// TODO: Add colors and icons and stuff
		Console.WriteLine($"'{Name}' command:");
		Console.WriteLine($"{Description}");
		Console.WriteLine();

		PrintUsage();
		Console.WriteLine();

		foreach (Argument arg in Arguments)
		{
			Console.WriteLine(arg.Name);
			Console.WriteLine(arg.Description);

			Console.WriteLine();
			Console.WriteLine($"Example: {arg.ExampleValue}");
			Console.WriteLine();

			if (arg.Optional) Console.WriteLine("This is an optional argument.");
			if (arg.CaseSensitive) Console.WriteLine("This is a case-sensitive optional argument.");
			Console.WriteLine();
		}
	}
}

struct Argument
{
	public string Name;
	public string Description;
	public string ExampleValue;

	public bool Optional;
	public bool CaseSensitive;

	public Argument(string name, string description, string exampleValue)
	{
		Name = name;
		Description = description;
		ExampleValue = exampleValue;
	}

	public Argument(string name, string description, string exampleValue, bool optional, bool caseSensitive = false)
	{
		Name = name;
		Description = description;
		ExampleValue = exampleValue;
		Optional = optional;
		CaseSensitive = caseSensitive;
	}

	// TODO: Use a property
	public string GetValue()
	{
		if (Optional) return $"[{Name}]";
		return $"<{Name}>";
	}
}