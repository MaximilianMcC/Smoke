public class Logger
{
	public static void Log(params string[] text)
	{
		foreach (string line in text)
		{
			Console.WriteLine("\u001b[31m" + text + "\u001b[0m");
		}
	}
}