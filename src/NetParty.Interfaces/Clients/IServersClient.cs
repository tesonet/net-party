using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Interfaces.Clients
{
    public interface IServersClient
    {
        Task<List<Server>> FetchServers(string token);
    }
}