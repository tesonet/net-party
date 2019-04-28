using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetParty.Application.Interfaces
{
    public interface IApi
    {
        Task<IList<IServer>> GetServers(string token);
        Task<string> GetAuthorizationToken(string username, string password);
    }
}
