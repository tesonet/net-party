#region Using

using System.Threading.Tasks;

#endregion

namespace NetParty.Application.Servers.ServerListApi
    {
    public interface IServerListApiTokenProvider
        {
        Task<string> GetTokenAsync();
        }
    }
