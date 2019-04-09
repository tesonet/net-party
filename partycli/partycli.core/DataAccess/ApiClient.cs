using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;
using partycli.core.Contracts;
using log4net;

namespace partycli.core.DataAccess
{
    public class ApiClient : IApiClient
    {
        ApiSettings _settings { get; set; }
        readonly ILog _logger;
        readonly HttpClient _client;

        public ApiClient()
        {
            //Default settings
            _settings = new ApiSettings();
            _logger = LogManager.GetLogger(GetType());
            _client = new HttpClient();
            Init();
        }

        void Init()
        {
            try
            {
                //Try to read from file
                using (TextReader reader = new StreamReader("./ApiSettings.json"))
                {
                    _settings = JsonConvert.DeserializeObject<ApiSettings>(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                _logger.Warn("Api settings not found. Using default settings.");
            }
        }
        
        public async Task<string> Get(string url)
        {
            _logger.Debug($"Getting from {url}...");
            try
            {
                return await (await _client.GetAsync(url)).EnsureSuccessStatusCode().Content.ReadAsStringAsync();
            }
            catch (HttpRequestException e)
            {
                _logger.Error($"GET request to {url} failed. {e.Message}");
                throw;
            }
        }

        public async Task<string> Post(string url, string jsonContent)
        {
            _logger.Debug($"Posting to {url}...");
            try
            {
                using (var content = new StringContent(jsonContent, Encoding.UTF8, "application/json"))
                {
                    return await (await _client.PostAsync(url, content)).EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                }
            }
            catch (HttpRequestException e)
            {
                _logger.Error($"POST request to {url} failed. {e.Message}");
                throw;
            }  
        }

        public async Task<IEnumerable<ServerContract>> GetServers(string authToken)
        {
            _logger.Debug("Getting server list...");

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            return JsonConvert.DeserializeObject<IEnumerable<ServerContract>>(await Get(_settings.ServerUri));
        }
        
        public async Task<string> GetToken(CredentialsContract credentials)
        {
            _logger.Debug("Getting token...");
            
            return JsonConvert.DeserializeObject<TokenContract>(
                await Post(_settings.TokenUri, JsonConvert.SerializeObject(credentials))).Token;
        }
    }
}
