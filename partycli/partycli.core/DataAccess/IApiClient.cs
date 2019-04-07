using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using partycli.core.Contracts;
using partycli.core.Repositories.Model;

namespace partycli.core.DataAccess
{
    public interface IApiClient
    {
        Task<string> Get(string url);
        Task<IEnumerable<ServerContract>> GetServers(string authToken);
        Task<string> Post(string url, string jsonContent);
        Task<string> GetToken(Credentials credentials);
    }
}