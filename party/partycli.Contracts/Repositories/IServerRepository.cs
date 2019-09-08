using System.Collections.Generic;
using System.Threading.Tasks;
using partycli.Contracts.Entities;

namespace partycli.Contracts.Repositories
{
    public interface IServerRepository
    {
        Task<IList<ServerEntity>> GetAllAsync();
        Task SaveAsync(IEnumerable<ServerEntity> servers);
        Task DeleteAllAsync();
    }
}
