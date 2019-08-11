using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net_party.Services.Server
{
    interface IServerService
    {
        Task<IEnumerable<Entities.Server>> UpdateLocalServerData(string token = null);
        Task<IEnumerable<Entities.Server>> GetServersLocally();
    }
}
