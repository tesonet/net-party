using System.Collections.Generic;
using System.Threading.Tasks;
using Net_party.CommandLineModels;
using Net_party.Entities;
using Net_party.Services.Server;

namespace Net_party.Controllers
{
    class ServerController
    {
        private readonly IServerService _serverService;
        public ServerController()
        {
           _serverService = new ServerService();
        }

        public Task<IEnumerable<Server>> GetServers(ServersRetrievalConfigurationDto data)
        {
            return data.IsLocal ? _serverService.GetServersLocally() : _serverService.UpdateLocalServerData();
        }
    }
}
