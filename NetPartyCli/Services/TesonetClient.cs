using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NetPartyCli.Dto;
using Newtonsoft.Json;

namespace NetPartyCli.Services
{
    public class TesonetClient
    {
        private readonly HttpClient _httpClient;
        private readonly TesonetSettings _settings;

        public TesonetClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new TesonetSettings();
        }
        
        public async Task<string> GetTokenAsync(string username, string password)
        {

            var json = JsonConvert.SerializeObject(new { username, password });
            var response = await _httpClient.PostAsync(_settings.TokenResource, new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(result);

            return token?.Token;
        }

        public async Task<IList<ServerDto>> GetAllAsync(string token)
        {
            var requestMsg = new HttpRequestMessage(HttpMethod.Get, _settings.ServerResource);
            requestMsg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMsg);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ServerDto>>(result);
        }

        private class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
