using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyCLI
{
    public interface IServerRequestsManager
    {
        Task<AccessToken> GetTokenAsync(UserData userCredentials);
        Task<List<Server>> GetServersAsync(string token);
    }
}
