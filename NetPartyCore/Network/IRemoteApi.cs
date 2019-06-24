using NetPartyCore.Datastore.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetPartyCore.Network
{
    interface IRemoteApi
    {
        Task<string> GetToken(Client client);

        Task<List<Server>> GetServers(string token);

    }
}
