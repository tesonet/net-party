using PartyCli.Core.Entities;
using PartyCli.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using PartyCli.Infrastructure.Exeptions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace PartyCli.Infrastructure.ServersApis
{
    public class WebServersApi : IServersApi
    {
        private HttpClient client = new HttpClient();
        private readonly IApiAuthCredentialsRepository _apiAuthCredentialsRepository;
        private readonly ILogger _logger;
        private readonly string _authApiUrl = "";
        private readonly string _serverApiUrl = "";

        private string _bearerToken = "";

        public WebServersApi(IApiAuthCredentialsRepository apiAuthCredentialsRepository, ILogger logger, IConfiguration configuration)
        {
            _apiAuthCredentialsRepository = apiAuthCredentialsRepository;
            _authApiUrl = configuration["AuthApiUrl"] ?? throw new PresentableConfigExeption("AuthApiUrl");
            _serverApiUrl = configuration["ServersApiUrl"] ?? throw new PresentableConfigExeption("ServersApiUrl");
            _logger = logger;
        }

        public async Task<List<Server>> GetServersAsync()
        {
            _logger.Debug("Started loading servers from web api.");

            var result = new List<Server>();

            await GetTokken();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( _bearerToken);

            HttpResponseMessage response = await client.GetAsync(_serverApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var stringContent = await response.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<List<dynamic>>(stringContent)
                            .Select(s => new Server() { Name = s.name, Distance = s.distance })
                            .ToList();

                _logger.Debug("Got servers list from web api.");

                return result;
            }


            _logger.Error("Could not get servers info.\n\r" +
                  $"Call to remote api failed with code: {response.StatusCode.ToString()}.\n\r" +
                  $"Reason: {response.ReasonPhrase}");

            throw new PresentableExeption("Call to remote api failed. Could not get servers. Check logs for details.");
        }

        private async Task GetTokken()
        {
            var credencials = _apiAuthCredentialsRepository
                                .GetAll()
                                .FirstOrDefault();

            if (credencials == null)
                throw new PresentableExeption("Could not load authorization credentials. Configure first.");

            var postBody = JsonConvert.SerializeObject(new { username = credencials.UserName, password = credencials.Password });               

            var response = await client.PostAsync(_authApiUrl, new StringContent(postBody, Encoding.UTF8,"application/json"));
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                dynamic tokkenObject = JsonConvert.DeserializeObject<dynamic>(result);
                _bearerToken = tokkenObject.token;

                _logger.Debug("Got access token.");

                return;
            }

            _logger.Error( "Could not get access token.\n\r" +
                          $"Call to remote api failed with code: {response.StatusCode.ToString()}.\n\r" +
                          $"Reason: {response.ReasonPhrase}");

            throw new PresentableExeption("Call to remote api failed. Could not get access token. Check logs for details.");
        }
    }
}
