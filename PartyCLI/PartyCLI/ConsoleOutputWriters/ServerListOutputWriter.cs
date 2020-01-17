namespace PartyCLI.ConsoleOutputWriters
{
    using System;

    using PartyCLI.ConsoleOutputWriters.Parameters;
    using PartyCLI.Data.Models;

    public class ServerListOutputWriter : IServerListOutputWriter
    {
        private readonly IOutputProvider outputProvider;

        public ServerListOutputWriter(IOutputProvider outputProvider)
        {
            this.outputProvider = outputProvider;
        }

        public void WriteLineToConsole(ServerListConsoleOutputParams parameters, params Server[] servers)
        {
            if (parameters == null)
            {
                parameters = new ServerListConsoleOutputParams();
            }

            Console.ForegroundColor = parameters.ForegroundColor;

            foreach (var server in servers)
            {
                outputProvider.WriteLine(parameters.GetOutputLineString(server));
            }
        }
    }
}