using System;
using System.Collections.Generic;
using NetPartyCore.Datastore.Model;

namespace NetPartyCore.Output
{
    class OutputFormatter : IOutputFormatter
    {
        public void PrintServers(List<Server> servers)
        {
            foreach (Server server in servers)
            {
                Console.WriteLine($"Server: {server.Name}");
            }

            Console.WriteLine($"Total servers: {servers.Count}");
        }

        public void TestMethod(string output)
        {
            Console.WriteLine($"OUTPUT: {output}");
        }
    }
}
