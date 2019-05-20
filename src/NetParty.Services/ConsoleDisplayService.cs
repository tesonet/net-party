using System;
using ConsoleTables;
using GuardNet;
using NetParty.Contracts;
using NetParty.Services.Interfaces;

namespace NetParty.Services
{

    public class ConsoleDisplayService : IDisplayService
    {
        public void DiplsayTable(ServerDto[] servers)
        {
            Guard.NotNull(servers, nameof(servers));

            var table = new ConsoleTable("Name");

            foreach (ServerDto server in servers)
            {
                table.AddRow(server.Name);
            }

            table.Write();
            Console.WriteLine();
        }
    }
}
