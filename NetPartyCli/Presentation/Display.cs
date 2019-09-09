using System;
using System.Collections.Generic;
using NetPartyCli.Dto;

namespace NetPartyCli.Presentation
{
    public class Display
    {
        public void Show(IEnumerable<ServerDto> servers)
        {
            foreach (var server in servers)
            {
                Console.WriteLine($"Server name: {server.Name}, distance: {server.Distance}.");
            }
        }
    }
}
