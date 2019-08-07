using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Domain.Models;

namespace NetParty.Domain.Interfaces
{
    public interface ILocalServerRepository
    {
        Task<IEnumerable<Server>> Get();
        Task Save(IEnumerable<Server> servers);
    }
}