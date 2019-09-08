using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using partycli.Contracts.Clients;
using partycli.Contracts.DTOs;
using partycli.Contracts.Exceptions;

namespace partycli.Clients
{
    public class TesonetClient: IServerListClient
    {
        private readonly HttpClient _httpClient;
        private readonly TesonetSettings _settings;

        public TesonetClient(HttpClient httpClient, TesonetSettings settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }
        
        public async Task<string> GetTokenAsync(string username, string password)
        {
            var response = await _httpClient.PostAsync(_settings.TokenResource, new JsonContent(new
            {
                username,
                password
            }));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(result);

            return token?.Token;
        }

        public async Task<IList<ServerDTO>> GetAllAsync(string token)
        {
            var requestMsg = new HttpRequestMessage(HttpMethod.Get, _settings.ServerResource);
            requestMsg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMsg);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ServerDTO>>(result);
        }

        private class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
