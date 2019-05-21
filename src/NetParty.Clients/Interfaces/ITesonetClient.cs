using NetParty.Contracts;
using System.Threading.Tasks;

namespace NetParty.Clients.Interfaces
{
    public interface ITesonetClient
    {
        Task<ServerDto[]> GetServersAsync(string token);

        Task<string> GetTokenAsync(string userName, string password);
    }
}
