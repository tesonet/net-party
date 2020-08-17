using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetParty.Domain.Servers
{
    public interface IServerService
    {
        Task<ICollection<Server>> GetAll(bool refresh);
    }
}
