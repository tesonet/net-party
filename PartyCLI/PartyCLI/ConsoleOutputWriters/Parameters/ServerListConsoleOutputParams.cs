namespace PartyCLI.ConsoleOutputWriters.Parameters
{
    using System;

    using PartyCLI.Data.Models;

    public class ServerListConsoleOutputParams
    {
        public ServerListConsoleOutputParams()
        {
            ForegroundColor = ConsoleColor.White;
            GetOutputLineString = s => $"{$"Server name: {s.Name}".PadLeft(ServerPaddingWidth, ' ')}, "
                                              + $"Distance: {s.Distance}";
        }

        public static int ServerPaddingWidth => 50;

        public ConsoleColor ForegroundColor { get; set; }

        public Func<Server, string> GetOutputLineString { get; set; }
    }
}