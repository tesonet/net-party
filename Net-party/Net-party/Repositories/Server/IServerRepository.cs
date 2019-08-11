using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net_party.Repositories.Server
{
    interface IServerRepository
    {
        Task SaveServers(List<Entities.Server> servers);
        Task<List<Entities.Server>> GetServers();
    }
}
