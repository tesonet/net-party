using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Domain.Models;

namespace NetParty.Domain.Interfaces
{
    public interface IServerRepository
    {
        Task<AuthorizationData> Authorize(string username, string password);
        Task<IEnumerable<Server>> Get();
    }
}