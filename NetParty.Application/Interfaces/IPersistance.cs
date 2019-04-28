using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetParty.Application.Interfaces
{
    public interface IPersistance
    {
        Task SaveCredentials(Domain.Entities.Credentials credentials);
        Task<Domain.Entities.Credentials> GetCredentials();
        Task SaveServers(IList<Domain.Entities.Server> list);
        Task<IList<Domain.Entities.Server>> GetServers();
    }
}
