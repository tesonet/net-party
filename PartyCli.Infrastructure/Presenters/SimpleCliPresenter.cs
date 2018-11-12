using PartyCli.Core.Entities;
using PartyCli.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Infrastructure.Presenters
{
    public class SimpleCliPresenter : IPresenter
    {
        public SimpleCliPresenter()
        { }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayServers(List<Server> servers)
        {  
            foreach (var server in servers)
            {
                Console.WriteLine(server.Name);
            }

            Console.WriteLine($"Servers count: {servers.Count}");
        }
    }
}
