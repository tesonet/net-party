using System;
using PartyCli.Contracts.Response;

namespace PartyCli.Output
{
	public class ConsoleOutputWriter : IConsoleOutputWriter
	{
		public void Write(ConsoleResponse response)
		{
			foreach (var (text, consoleColor) in response.Lines)
			{
				Console.ForegroundColor = consoleColor;
				Console.WriteLine(text);
				Console.ResetColor();
			}
		}
	}
}