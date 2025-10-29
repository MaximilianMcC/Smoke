class Program
{
	public static void Main(string[] args)
	{
		// Ensure we have supplied args
		if (args.Length == 0)
		{
			Console.WriteLine("No arguments supplied. Please either make a new project, or open an existing project:");
			Console.WriteLine("smoke new <PROJECT NAME>");
			Console.WriteLine("smoke <PROJECT NAME>");
			return;
		}

		// Check for if we are opening the editor or making a new project
		if (args[0].ToLower().Trim() == "new")
		{
			// Get the new projects name
			if (args.Length < 1)
			{
				Console.WriteLine("Please supply a project name. For example:");
				Console.WriteLine("smoke new \"MyAwesomeGame\"");
				return;
			}
			string projectName = args[1].Trim();

			// Make the new project
			ProjectMaker.CreateNewProject(projectName);
		}
		else
		{
			// Opening an existing project
		}
	}
}