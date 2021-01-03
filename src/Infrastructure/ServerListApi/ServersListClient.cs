namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Application;
    using Http;
    using Server = Domain.Server;

    internal class ServersListClient : IServersListClient
    {
        private readonly HttpClient _httpClient;

        public ServersListClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyCollection<Server>> GetAll()
        {
            var response = await _httpClient.GetAsync("servers");
            response.EnsureSuccessStatusCode();

            var servers = await response
                .Content
                .ReadFromJsonAsync<IReadOnlyCollection<Server>>(JsonOptions.Default);

            return servers!;
        }
    }
}