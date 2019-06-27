using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace NetPartyCore.Network
{
    interface IRemoteApi
    {
        [Post("/tokens")]
        Task<TokenResponse> GetToken(string username, string password);

        [Get("/servers")]
        Task<List<ServersResponse>> GetServers([Header("Authorization")] string authorization);
    }
}
