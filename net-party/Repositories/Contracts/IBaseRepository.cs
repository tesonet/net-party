using net_party.Entities.Interfaces;
using System.Threading.Tasks;

namespace net_party.Repositories.Contracts
{
    public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        Task<long> Add(T entity);

        Task<bool> Update(T entity);
    }
}