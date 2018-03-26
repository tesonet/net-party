using PartyCli.Interfaces;
using PartyCli.Models;
using System;
using System.Collections.Generic;

namespace PartyCli.Presenters
{
    public class ServerPresenter : IServerPresenter
    {
        public void Display(IEnumerable<Server> servers)
        {
            int count = 0;
            foreach (var server in servers)
            {
                Console.WriteLine($"Name: {server.Name}");
                count++;
            }
            Console.WriteLine($"Number of servers: {count}");
        }
    }
}
