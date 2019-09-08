using System;
using System.Collections.Generic;
using partycli.Contracts.DTOs;
using partycli.Contracts.Services;

namespace partycli.Services
{
    public class ConsoleUi: IAppUi
    {
        public void Show(IEnumerable<ServerDTO> servers)
        {
            Console.WriteLine("Server list.");

            foreach (var server in servers)
            {
                Console.WriteLine($"Name: {server.Name}, distance: {server.Distance}.");
            }
        }

        public void Show(string message)
        {
            Console.WriteLine(message);
        }
    }
}
