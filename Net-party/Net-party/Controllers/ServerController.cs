using System.Collections.Generic;
using System.Threading.Tasks;
using Net_party.CommandLineModels;
using Net_party.Entities;
using Net_party.Services.Server;

namespace Net_party.CommandLineControllers
{
    class ServerController
    {
        private IServerService _serverService;
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
