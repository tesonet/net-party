using net_party.Entities.Database;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace net_party.Repositories.Contracts
{
    public interface IServerRepository : IBaseRepository<Server>
    {
        Task<IEnumerable<Server>> Get();

        Task Truncate(IDbTransaction transaction = null);

        Task<long> AddMany(IEnumerable<Server> entity, IDbTransaction transaction = null);
    }
}