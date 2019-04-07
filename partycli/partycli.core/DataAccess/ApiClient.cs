using partycli.core.Repositories.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using partycli.core.Logging;
using System.Net.Http.Headers;
using System.IO;
using partycli.core.Contracts;

namespace partycli.core.DataAccess
{
    public class ApiClient : IApiClient
    {
        IApiSettings _settings;
        ILogger _logger;
        HttpClient _client;

        public ApiClient(ILogger logger)
        {
            Init();
            _logger = logger;
            _client = new HttpClient();
        }

        void Init()
        {
            using (TextReader reader = new StreamReader("./ApiRoutes.json"))
            {
                string json = reader.ReadToEnd();
                _settings = JsonConvert.DeserializeObject<ApiSettings>(json);
            }
        }
        
        public async Task<string> Get(string url)
        {
            _logger.Debug($"Getting from {url}...");
            var responseMessage = await _client.GetAsync(url);
            responseMessage.EnsureSuccessStatusCode();
            return await responseMessage.Content.ReadAsStringAsync();
        }

        public async Task<string> Post(string url, string jsonContent)
        {
            _logger.Debug($"Posting to {url}...");
            using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
            { 
                var responseMessage = await _client.PostAsync(url, content);
                responseMessage.EnsureSuccessStatusCode();
                return await responseMessage.Content.ReadAsStringAsync();
            }   
        }

        public async Task<IEnumerable<ServerContract>> GetServers(string authToken)
        {
            _logger.Debug("Getting server list...");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var serverListJson = await Get(_settings.ServerUri);

            return JsonConvert.DeserializeObject<IEnumerable<ServerContract>>(serverListJson);
        }
        
        public async Task<string> GetToken(Credentials credentials)
        {
            _logger.Debug("Getting token...");
            
            var credsJson = JsonConvert.SerializeObject(credentials).ToLowerInvariant();
            var token = JsonConvert.DeserializeObject<TokenContract>(await Post(_settings.TokenUri, credsJson));
            
            return token.Token;
        }
    }
}
