using System.Collections.Generic;
using System.Threading.Tasks;
using partycli.Contracts.DTOs;

namespace partycli.Contracts.Clients
{
    public interface IServerListClient
    {
        Task<string> GetTokenAsync(string username, string password);
        Task<IList<ServerDTO>> GetAllAsync(string token);
    }
}
