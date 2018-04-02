using PartyCli.Interfaces;
using PartyCli.Models;
using Serilog;
using System;
using System.Collections.Generic;

namespace PartyCli.Presenters
{
    public class ServerPresenter : IServerPresenter
    {
        ILogger logger;

        public ServerPresenter(ILogger logger)
        {
            this.logger = logger?.ForContext<ServerPresenter>() ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Display(IEnumerable<Server> servers)
        {
            int count = 0;
            foreach (var server in servers)
            {
                Console.WriteLine($"Name: {server.Name}");
                count++;
            }
            Console.WriteLine($"Number of servers: {count}");
            logger.Information("{NumberOfServers} of servers displayed", count);
        }
    }
}
