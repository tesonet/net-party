using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PartyCli.WebApiClient.DataContracts;
using PartyCli.WebApiClient.Interfaces;

namespace PartyCli.WebApiClient
{
  public class WebApiClient : IWebApiClient
  {
    private static readonly HttpClient _httpClient = new HttpClient();
    private readonly ILogger _logger;
    private readonly IWebApiClientSettings _settings;

    public WebApiClient(IWebApiClientSettings settings, ILogger logger)
    {
      _logger = logger;
      _settings = settings;
    }

    public async Task<ICollection<ServerDataContract>> GetServersAsync(string token)
    {
      ServerDataContract[] servers;

      if (string.IsNullOrWhiteSpace(_settings.WebApiBaseUrl))
        throw new Exception("Incorrect application settings. WebApiBaseUrl isn't configured.");

      var url = $"{_settings.WebApiBaseUrl}/v1/servers";

      _logger.Info($"Downloading Servers from {url}");

      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
      using (var response = await _httpClient.GetAsync(url))
      {
        _logger.Info($"Response StatusCode: {response.StatusCode}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();
        servers = JsonConvert.DeserializeObject<ServerDataContract[]>(jsonString);
      }
      _logger.Info($"Servers list download done. Total count: {servers.Count()}");

      return servers;
    }

    public async Task<TokenDataContract> GetTokenAsync(string username, string password)
    {
      TokenDataContract token;

      if (string.IsNullOrWhiteSpace(_settings.WebApiBaseUrl))
        throw new Exception("Incorrect application settings. WebApiBaseUrl isn't configured.");

      var url = $"{_settings.WebApiBaseUrl}/v1/tokens";

      _logger.Info($"Request access token from {url}");

      var jsonBody = JsonConvert.SerializeObject(new { username, password });
      using (var content = new StringContent(jsonBody, Encoding.UTF8, "application/json"))
      {
        var response = await _httpClient.PostAsync(url, content);

        _logger.Info($"Response StatusCode: {response.StatusCode}");
        response.EnsureSuccessStatusCode();

        var jsonString = await response.Content.ReadAsStringAsync();

        token = JsonConvert.DeserializeObject<TokenDataContract>(jsonString);
      }

      _logger.Info($"Token is valid: { (string.IsNullOrWhiteSpace(token?.Token) ? "No" : "Yes")}");
      return token;
    }
  }
}
