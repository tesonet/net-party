using net_party.Entities.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace net_party.Services.Contracts
{
    public interface IServerService
    {
        Task<IEnumerable<Server>> GetServers(bool local = false);
    }
}