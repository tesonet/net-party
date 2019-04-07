using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using partycli.core.Repositories.Model;

namespace partycli.Presentation
{
    class ConsoleWriter : IConsoleWriter
    {
        IConsoleOutputParams _outputParams;
        string _separator = new string('-', 50);
        
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

            foreach (var server in servers)
            {
                Write($"{server.Name} {server.Distance}");
            }

            Write(_separator);
            Write($"Total servers: {servers.Count()}");
        } 
    }
}
