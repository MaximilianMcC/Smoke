static class ArgumentParser
{
	public static bool CommandRan(Command command, string[] args)
	{
		// Check for if they want help on the command
		if (args[0].ToLower() == "help")
		{
			command.PrintFullUsage();
			return false;
		}

		// Check for if the command name is the same
			if (command.Name != args[0].ToLower()) return false;

		// Check for if the correct amount of arguments are present
		if ((args.Length < command.MinLength) || (args.Length > command.MaxLength))
		{
			// TODO: Add colors and icons and stuff
			Console.Error.WriteLine("Invalid argument count!");
			Console.Error.WriteLine($"Type 'smoke {command.Name} help' for advanced help");
			command.PrintUsage();
		}

		int index = 0;
		return true;
	}
}

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
	}

	//? 1 is added for the command name
	public int MaxLength => 1 + Arguments.Length;
	public int MinLength => 1 + Arguments.Where(arg => arg.Optional).Count();

	public void PrintUsage()
	{
		// TODO: Add colors and icons and stuff
		Console.WriteLine("Example usage:");

		Console.Write($"{Name} ");
		foreach (Argument arg in Arguments) Console.Write($"{arg.GetValue()} ");
		Console.WriteLine();

		Console.Write($"{Name} ");
		foreach (Argument arg in Arguments) Console.Write($"{arg.GetExample()} ");
		Console.WriteLine();
	}

	public void PrintFullUsage()
	{
		// TODO: Add colors and icons and stuff
		Console.WriteLine($"'{Name}' command:");
		Console.WriteLine($"{Description}");
		Console.WriteLine();

		PrintFullUsage();
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
	public string GetValue() => GetValue(false);
	public string GetExample() => GetValue(true);

	private string GetValue(bool example)
	{
		string value = example ? ExampleValue : Name;

		if (Optional) return $"[{value}]";
		return $"<{value}>";
	}
}