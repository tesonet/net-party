using net_party.Entities.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace net_party.Repositories.Contracts
{
    public interface IServerRepository : IBaseRepository<Server>
    {
        Task<IEnumerable<Server>> Get();

        Task<long> AddMany(IEnumerable<Server> entity);
    }
}
