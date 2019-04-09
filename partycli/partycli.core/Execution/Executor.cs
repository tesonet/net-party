using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using partycli.core.DataAccess;
using partycli.core.Repositories.Model;
using partycli.core.Contracts;
using partycli.core.Repositories.Storage;
using log4net;

namespace partycli.core.Execution
{
    public class Executor : IExecutor
    {
        readonly IApiClient _apiClient;
        readonly IStorageManager _storageManager;
        readonly ILog _logger;

        public Executor(IApiClient apiClient, IStorageManager storageManager)
        {
            _logger = LogManager.GetLogger(GetType());
            _apiClient = apiClient;
            _storageManager = storageManager;
        }

        public async Task<IEnumerable<Server>> FetchServers(bool local)
        {
            IEnumerable<Server> servers = null;

            if (local)
            {
                servers = _storageManager.GetServers();
            }
            else
            {
                var credentials = _storageManager.GetCredentials();

                var token = await _apiClient.GetToken(
                    new CredentialsContract() {
                        Username = credentials.Username,
                        Password = credentials.Password
                    });
                var serverContracts = await _apiClient.GetServers(token);

                servers = serverContracts.Select(c => new Server() { Name = c.Name, Distance = c.Distance }).ToList();
                _storageManager.StoreServers(servers);
            }
            
            return servers;
        }

        public void SaveCredentials(Credentials credentials)
        {
            _storageManager.SaveCredentials(credentials);
        }
    }
}
