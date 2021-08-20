using log4net;
using partycli.Entities;
using partycli.Models.Reporsitory;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
namespace partycli.Models.Service
{
    public class ServiceList : IService
    {
        private readonly IRepository _repository;
        private readonly IWebService _apiService;
        public ServiceList(IWebService apiService, IRepository repository)
        {
            _apiService = apiService;
            _repository = repository;
        }

        public SaveUsersResponse SaveCredentials(Credentials args)
        {
            if (string.IsNullOrEmpty(args.Username) || string.IsNullOrEmpty(args.Password))
                return new SaveUsersResponse { Message = "Username and Password can not be empty." };

            _repository.SaveUserInfo(args);
            return new SaveUsersResponse { Success = true };
        }

        public GetServersListResponse GetServersList(GetServersInfo args)
        {
            var servers = new List<ServerList>();
            if (args.UseLocal)
            {
                servers = _repository.GetServers();
                if (servers?.Count == 0)
                {
                    return new GetServersListResponse { Message = "Servers in database is empty." };
                }
            }
            else
            {
                var credentials = _repository.GetUserInfo();
                if (credentials == null)
                {
                    return new GetServersListResponse { Message = "No users in database." };
                }
                var tokenResponse = _apiService.GenerateToken(credentials);
                if (!tokenResponse.Success)
                {
                    return new GetServersListResponse { Message = tokenResponse.Message };
                }
                var serversResponse = _apiService.GetServersList(new GetWebServerServerListArgs { Token = tokenResponse.Token });
                if (!serversResponse.Success)
                {
                    return new GetServersListResponse { Message = serversResponse.Message };
                }

                servers = serversResponse.Servers;
                _repository.SaveServers(servers);
            }
                   

            return new GetServersListResponse { Success = true, Servers = servers };
        }
    }
}