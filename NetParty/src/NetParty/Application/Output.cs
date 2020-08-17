using System;

namespace NetParty.Application
{
    internal static class Output
    {
        public static void Error(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Info(string value)
        {
            Console.WriteLine(value);
        }
    }
}
