using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Contracts;

namespace NetParty.Interfaces.Repositories
{
    public interface IServersRepository
    {
        Task<List<Server>> GetServers();
        Task SaveServers(List<Server> servers);
    }
}