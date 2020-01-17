namespace PartyCLI.ConsoleOutputWriters
{
    using System;

    public class ConsoleOutputProvider : IOutputProvider
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}