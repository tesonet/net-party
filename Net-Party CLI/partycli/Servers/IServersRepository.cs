using System.Collections.Generic;
using System.Threading.Tasks;

namespace partycli.Servers
{
    public interface IServersRepository
    {
        Task<List<Server>> RetrieveServersListAsync(string token);
        Task<List<Server>> RetrieveServersListLocalAsync();
    }
}
