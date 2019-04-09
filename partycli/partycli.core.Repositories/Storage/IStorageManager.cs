using partycli.core.Repositories.Model;
using System.Collections.Generic;

namespace partycli.core.Repositories.Storage
{
    public interface IStorageManager
    {
        Credentials GetCredentials();
        IEnumerable<Server> GetServers();
        void SaveCredentials(Credentials credentials);
        void StoreServers(IEnumerable<Server> servers);
    }
}