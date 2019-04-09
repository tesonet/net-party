using System;
using System.Collections.Generic;
using System.Linq;
using partycli.core.Repositories.Model;

namespace partycli.Presentation
{
    class ConsoleWriter : IConsoleWriter
    {
        static readonly string _separator = new string('-', 50);
        IConsoleOutputParams _outputParams;
        
        public ConsoleWriter(IConsoleOutputParams outputParams)
        {
            _outputParams = outputParams;
        }
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void Write(IEnumerable<Server> servers)
        {
            Write(_separator);
            Write($"{"Servers",-25} {"Distances",24}");
            Write(_separator);

            foreach (var server in servers)
            {
                Write($"{server.Name,-25} {server.Distance, 24}");
            }

            Write(_separator);
            Write($"Total servers: {servers.Count()}");
        } 
    }
}
