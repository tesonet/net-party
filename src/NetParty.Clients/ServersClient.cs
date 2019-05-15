using System.Collections.Generic;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Interfaces.Clients;
using Pathoschild.Http.Client;

namespace NetParty.Clients
{
    public class ServersClient : IServersClient
    {
        private readonly IClient _client;

        public ServersClient(IClient client)
        {
            _client = client;
        }

        public Task<List<Server>> FetchServers(string token)
        {
            return _client.GetAsync("v1/servers")
                .WithBearerAuthentication(token)
                .As<List<Server>>();
        }
    }
}