using partycli.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace partycli.Servers
{
    public interface IServersRepository
    {
        Task<IRequestResult<List<Server>>> RetrieveServersListAsync(string token);
        Task<List<Server>> RetrieveServersListLocalAsync();
    }
}
