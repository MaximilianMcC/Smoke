using System.Reflection;
using Smoke;

class Program
{
	public static void Main(string[] args)
	{
		// Load the smoke project from argument
		SmokeProject.Instance.Load(args[0]);

		//! debug print all loaded assemblies
		foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
		{
			Console.WriteLine(assembly.Location);
		}
			
	}
}