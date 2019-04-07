using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using partycli.core.DataAccess;
using partycli.core.Repositories.Model;
using partycli.core.Logging;
using partycli.core.Contracts;
using partycli.core.Repositories.Storage;

namespace partycli.core.Execution
{
    public class Executor : IExecutor
    {
        IApiClient _apiClient;
        IStorageManager _storageManager;
        ILogger _logger;

        public Executor(IApiClient apiClient, IStorageManager storageManager, ILogger logger)
        {
            _apiClient = apiClient;
            _storageManager = storageManager;
            _logger = logger;
        }

        public async Task<IEnumerable<Server>> FetchServers(bool local)
        {
            IEnumerable<Server> servers = null;

            if (local)
            {
                try
                {
                    servers = _storageManager.GetServers();
                }
                catch (Exception e)
                {
                    _logger.Error("Error fetching from local repository.", e);
                }
            }
            else
            {
                try
                {
                    var credentials = _storageManager.GetCredentials();
                    var token = await _apiClient.GetToken(credentials);
                    var serverContracts = await _apiClient.GetServers(token);

                    servers = serverContracts.Select(c => new Server() { Name = c.Name, Distance = c.Distance });
                    _storageManager.StoreServers(servers);
                }

                catch (Exception e)
                {
                    _logger.Error("Error fetching servers.", e);
                    throw;
                }
            }
            
            return servers;
        }

        public void SaveCredentials(Credentials credentials)
        {
            try
            {
                _storageManager.SaveCredentials(credentials);
            }
            catch (Exception)
            {
                _logger.Error("Error saving credentials.");
                throw;
            }
        }
    }
}
