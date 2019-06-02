#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace NetParty.Application.Servers
    {
    public class ConsoleServerDisplayer : IServerDisplayer
        {
        private readonly IServerProvider m_serverProvider;

        public ConsoleServerDisplayer(IServerProvider serverProvider)
            {
            m_serverProvider = serverProvider ?? throw new ArgumentNullException(nameof(serverProvider));
            }

        public async Task DisplayServers()
            {
            IEnumerable<Server> serverList = await m_serverProvider.GetServersAsync();
            if (serverList.Any())
                {
                Console.WriteLine($"This is the server list({serverList.Count()}):");
                foreach (var server in serverList)
                    {
                    Console.WriteLine(server);
                    }
                }
            else
                {
                Console.WriteLine("No servers found");
                }
            }
        }
    }
