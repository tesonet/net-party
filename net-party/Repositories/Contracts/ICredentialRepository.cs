using net_party.Entities.Database;
using System.Threading.Tasks;

namespace net_party.Repositories.Contracts
{
    public interface ICredentialRepository : IBaseRepository<Credential>
    {
        Task<Credential> Get(string username);
    }
}
