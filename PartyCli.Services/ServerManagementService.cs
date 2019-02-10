using Castle.Core.Logging;
using PartyCli.Entities;
using PartyCli.Repositories;
using PartyCli.Services.Interfaces;
using PartyCli.WebApiClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyCli.Services
{
  public class ServerManagementService : IServerManagementService
  {
    private readonly ILogger _logger;
    private readonly IWebApiClient _webApi;
    private readonly IRepository<Credentials> _credentialsRepository;
    private readonly IRepository<Server> _serverRepository;

    public ServerManagementService(IWebApiClient webApi,
      IRepository<Credentials> credentialsRepository,
      IRepository<Server> serverRepository,
      ILogger logger)
    {
      _credentialsRepository = credentialsRepository;
      _serverRepository = serverRepository;
      _logger = logger;
      _webApi = webApi;
    }

    public async Task<ICollection<Server>> FetchServersAsync(bool local)
    {
      if (local)
      {
        _logger.Info("Fetching servers from data storage");
        return _serverRepository.FindAll();
      }
      else
      {
        var token = await GetAccessToken();
        var serverDataContracts = await _webApi.GetServersAsync(token);

        var servers = serverDataContracts.Select(x => new Server()
        {
          Name = x.Name,
          Distance = x.Distance,
        }).ToList();

        SaveServers(servers);

        return servers;
      }
    }

    public void SaveCredentials(string username, string password)
    {
      _logger.Info("Saving credentials to data storage");
      _credentialsRepository.Truncate();

      _credentialsRepository.InsertBulk(new List<Credentials>()
        {
          new Credentials()
          {
            UserName = username,
            Password = password,
          }
        });
      _logger.Info("Credentials saved succesfully");
    }

    private async Task<string> GetAccessToken()
    {
      var credentials = _credentialsRepository.FindAll();

      if (!credentials.Any())
        throw new Exception($"Credentials are not cofigured");

      var tokenCredentials = credentials.First();
      var token = await _webApi.GetTokenAsync(tokenCredentials.UserName, tokenCredentials.Password);

      if (string.IsNullOrWhiteSpace(token.Token))
        throw new Exception("Token from API is empty");

      return token.Token;

    }

    public void SaveServers(IEnumerable<Server> servers)
    {
      ClearServers();
      _logger.Info("Saving servers to data storage");
      _serverRepository.InsertBulk(servers);
    }

    public void ClearServers()
    {
      _logger.Info("Deleting all servers from data storage");
      _serverRepository.Truncate();
    }
  }
}
