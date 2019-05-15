using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsoleTables;
using NetParty.Contracts;
using NetParty.Interfaces.Services;

namespace NetParty.Services
{
    public class OutputService : IOutputService
    {
        public async Task OutputServers(List<Server> servers)
        {
            await Task.Run(() =>
            {
                if (!servers.Any())
                {
                    Console.WriteLine("No servers to display.");
                }
                else
                {
                    var table = new ConsoleTable("Name", "Distance");
                    servers.ForEach(s => table.AddRow(s.Name, s.Distance));
                    table.Write(Format.Alternative);
                    Console.WriteLine($"Servers count: {servers.Count}");
                }
            });
        }

        public async Task OutputStringLine(string text)
        {
            await Task.Run(() => { Console.WriteLine(text); });
        }
    }
}