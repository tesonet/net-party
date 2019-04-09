using System.Collections.Generic;
using System.Threading.Tasks;
using partycli.core.Repositories.Model;

namespace partycli.core.Execution
{
    public interface IExecutor
    {
        Task<IEnumerable<Server>> FetchServers(bool local);
        void SaveCredentials(Credentials credentials);
    }
}