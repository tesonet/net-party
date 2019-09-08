using System.Collections.Generic;
using System.Threading.Tasks;
using partycli.Contracts.DTOs;

namespace partycli.Contracts.Services
{
    public interface IServerLedger
    {
        Task<IEnumerable<ServerDTO>> GetAllAsync(bool usePersistedData);
        Task AddAsync(IList<ServerDTO> servers);
    }
}
