using System;
using System.Threading.Tasks;

namespace partycli.Servers
{
    class ServersHandler
    {
        IServersRepository m_serversService = null;

        public ServersHandler(IServersRepository serversService)
        {
            m_serversService = serversService;
        }

        public async Task GetServersListAsync(Task<string> token)
        {
            var result = await m_serversService.RetrieveServersListAsync(await token);
            foreach(var server in result)
            {
                Console.WriteLine(server.Name + " " + server.Distance);
            }
            Console.ReadKey();
        }

        public async Task GetServersListLocal()
        {
            var result = await m_serversService.RetrieveServersListLocalAsync();
            foreach (var server in result)
            {
                Console.WriteLine(server.Name + " " + server.Distance);
            }
            Console.ReadKey();
        }
    }
}
