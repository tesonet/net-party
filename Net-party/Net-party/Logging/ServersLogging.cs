using System;
using System.Collections.Generic;
using Net_party.Entities;

namespace Net_party.Logging
{
    static class ServersLogging
    {
        public static void LogToConsole(this IEnumerable<Server> servers)
        {
            foreach (var server in servers)
            {

                if (server.Distance > 1000)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                } else if (server.Distance > 500)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                }

                Console.WriteLine(server);
            }
            Console.ResetColor();
        }
    }
}
