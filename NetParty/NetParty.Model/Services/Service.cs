using NetParty.Common.Helpers;
using NetParty.Common.Log;
using NetParty.Model.Entities;
using NetParty.Model.Entities.Enums;
using NetParty.Model.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace NetParty.Model.Services
{
    public class Service : IService
    {
        private readonly ILog _log;
        private readonly IRepository _repository;
        private readonly IApiService _apiService;
        public Service(ILogProvider logProvider, IApiService apiService, IRepository repository)
        {
            _log = logProvider.Get<Service>(nameof(Service));
            _apiService = apiService;
            _repository = repository;
        }

        public SaveCredentialsResponse SaveCredentials(Credentials args)
        {
            _log.Debug($"SaveCredentials({args.JsonToString()})");
            if (string.IsNullOrEmpty(args.Username) || string.IsNullOrEmpty(args.Password))
                return new SaveCredentialsResponse { Message = "Username and Password can not be empty." };

            _repository.SaveCredentials(args);
            return new SaveCredentialsResponse { Success = true };
        }

        public GetServersResponse GetServersList(GetServersArgs args)
        {
            _log.Info($"GetServersList ({args.JsonToString()}");
            var servers = new List<Server>();
            if (args.UseLocal)
            {
                servers = _repository.GetServersList();
                if (servers?.Count == 0)
                {
                    _log.Warn("GetServersList ({0}): Servers list in database is empty.", args.JsonToString());
                    return new GetServersResponse { Message = "Servers list in database is empty." };
                }
            }
            else
            {
                var credentials = _repository.GetCredentials();
                if (credentials == null)
                {
                    _log.Warn("GetServersList ({0}): No credentials in database.", args.JsonToString());
                    return new GetServersResponse { Message = "No credentials in database." };
                }

                _log.Debug($"GetServersList.GenerateToken ({credentials.JsonToString()})");
                var tokenResponse = _apiService.GenerateToken(credentials);
                if (!tokenResponse.Success)
                {
                    _log.Error("GetServersList.GenerateToken ({0})", tokenResponse.Message);
                    return new GetServersResponse { Message = tokenResponse.Message };
                }

                _log.Info("GetServersList.CallApi (token={0})", tokenResponse.Token);
                var serversResponse = _apiService.GetServersList(new GetApiServersArgs { Token = tokenResponse.Token });
                if (!serversResponse.Success)
                {
                    _log.Error("GetServersList.ApiService.GetServersList ({0})", tokenResponse.Message);
                    return new GetServersResponse { Message = serversResponse.Message };
                }

                servers = serversResponse.Servers;
                _repository.SaveServerList(servers);
            }

            #region Extra: Ordering
            if (args.Order == Order.Asc)
                servers = servers.OrderBy(x => x.Distance).ToList();
            if (args.Order == Order.Desc)
                servers = servers.OrderByDescending(x => x.Distance).ToList();
            #endregion

            return new GetServersResponse { Success = true, Servers = servers };
        }
    }
}