using partycli.core.Repositories.Model;

namespace partycli.core.Repositories.Storage
{
    public interface IStorageManager
    {
        Credentials GetCredentials();
        System.Collections.Generic.IEnumerable<Server> GetServers();
        void SaveCredentials(Credentials credentials);
        void StoreServers(System.Collections.Generic.IEnumerable<Server> servers);
    }
}