using PartyCli.Interfaces;
using PartyCli.Models;
using System;
using System.Collections.Generic;

namespace PartyCli.Presenters
{
    public class ServersPresenter : IServersPresenter
    {
        public void Display(IEnumerable<Server> servers)
        {
            int count = 0;
            foreach (var server in servers)
            {
                Console.WriteLine($"Name: {server.Name}, Distance: {server.Distance}");
                count++;
            }
            Console.WriteLine($"Number of servers: {count}");
        }
    }
}
