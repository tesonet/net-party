using System;
using NetParty.Contracts.Results;
using NetParty.Domain.Models;

namespace NetParty.CLI.ResultsPrinter
{
    public class ConsoleResultsPrinter : IResultsPrinter
    {
        public const int TableSeparatorLineWidth = 61;

        public void Print(ServerList servers)
        {
            PrintSeparatorLine();
            PrintTableHeaders();
            PrintSeparatorLine();
            PrintServerList(servers);
            PrintSeparatorLine();
            PrintTotalServerCount(servers.Count);
        }

        public void Print(AuthorizationResult authorizationResult)
        {
            Console.WriteLine(authorizationResult.Message);
        }

        public void Print(Error error)
        {
            Console.Error.WriteLine($"[{error.Reason}] Error: {error.Message}");
        }

        private void PrintTotalServerCount(int serversCount)
        {
            Console.WriteLine($"Total server count: {serversCount}");
        }

        private void PrintTableHeaders()
        {
            object[] headers = {"Server Name", "Distance"};
            var header = string.Format("|{0,-40}|{1,-20}|", headers);

            Console.WriteLine(header);
        }

        private void PrintSeparatorLine()
        {
            Console.WriteLine($"|{new string('-', TableSeparatorLineWidth)}|");
        }

        private void PrintServerList(ServerList servers)
        {
            foreach (var server in servers.Items)
            {
                PrintLine(server);
            }
        }

        private void PrintLine(Server server)
        {
            Console.WriteLine($"|{server.Name,-40}|{server.Distance,-20}|");
        }
    }
}