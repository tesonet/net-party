using System;
using System.Collections.Generic;
using NetPartyCore.Datastore.Model;

namespace NetPartyCore.Output
{
    class OutputFormatter : IOutputFormatter
    {
        public void PrintConfiguration(Client client)
        {
            Console.WriteLine($"Client username is: {client.Username}");
            Console.WriteLine($"Client password is: {client.Password}");
        }

        public void PrintServers(List<Server> servers)
        {
            foreach (Server server in servers)
            {
                Console.WriteLine($"Server: {server.Name}");
            }

            Console.WriteLine($"Total servers: {servers.Count}");
        }
    }
}
