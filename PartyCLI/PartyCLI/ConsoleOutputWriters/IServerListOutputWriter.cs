namespace PartyCLI.ConsoleOutputWriters
{
    using PartyCLI.ConsoleOutputWriters.Parameters;
    using PartyCLI.Data.Models;

    public interface IServerListOutputWriter
    {
        void WriteLineToConsole(ServerListConsoleOutputParams parameters, params Server[] servers);
    }
}