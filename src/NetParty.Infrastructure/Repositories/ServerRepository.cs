using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NetParty.Domain.Exceptions;
using NetParty.Domain.Interfaces;
using NetParty.Domain.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetParty.Infrastructure.Repositories
{
    public class ServerRepository : IServerRepository
    {
        private readonly ILocalConfigurationRepository _localConfigurationRepository;
        private readonly HttpClient _httpClient;

        public ServerRepository(HttpClient httpClient, ILocalConfigurationRepository localConfigurationRepository)
        {
            _httpClient = httpClient;
            _localConfigurationRepository = localConfigurationRepository;
        }

        public async Task<AuthorizationData> Authorize(string username, string password)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("v1/tokens", UriKind.Relative),
                Method = HttpMethod.Post,
                Content = CreateRequestContents(new {Username = username, Password = password})
            };

            var response = await _httpClient.SendAsync(request);

            HandleNotSuccessfulRequests(response);

            var responseContent = await GetResponseContent<AuthorizationResponse>(response).ConfigureAwait(false);
            return new AuthorizationData {Username = username, Token = responseContent.Token};
        }

        public async Task<IEnumerable<Server>> Get()
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri("v1/servers", UriKind.Relative),
                Method = HttpMethod.Get,
                Headers = { Authorization = new AuthenticationHeaderValue("Bearer", await GetAuthorizationHeaderValue())}
            };

            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);

            HandleNotSuccessfulRequests(response);

            var servers = await GetResponseContent<IEnumerable<Server>>(response).ConfigureAwait(false);
            return servers;
        }

        private async Task<string> GetAuthorizationHeaderValue()
        {
            var authorizationData = await _localConfigurationRepository.GetAuthorizationData().ConfigureAwait(false);
            var token = authorizationData.Token;
            return token;
        }

        private void HandleNotSuccessfulRequests(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized: throw DomainExceptions.NotAuthorized;
                    default: throw DomainExceptions.CouldNotRetrieveServers;
                }
            }
        }

        private async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
            var content = JsonConvert.DeserializeObject<T>(stringResponseContent);

            return content;
        }

        private StringContent CreateRequestContents(object body)
        {
            return new StringContent(
                JsonConvert.SerializeObject(body,
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }), Encoding.UTF8,
                "application/json");
        }

        private class AuthorizationResponse
        {
            public string Token { get; set; }
        }
    }
}