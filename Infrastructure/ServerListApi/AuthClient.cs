namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Net.Mime;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Http;
    using JetBrains.Annotations;

    internal class AuthClient : IAuthClient
    {
        private readonly ClientCredentials _credentials;
        private readonly HttpClient _httpClient;

        public AuthClient(ClientCredentials credentials, HttpClient httpClient)
        {
            _credentials = credentials;
            _httpClient = httpClient;
        }

        public async Task<string> Authorize()
        {
            if (_credentials == ClientCredentials.Empty)
            {
                throw new ArgumentException("Missing API credentials.");
            }

            using var content = new StringContent(
                JsonSerializer.Serialize(_credentials, JsonOptions.Default),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using var response = await _httpClient.PostAsync("tokens", content);
            response.EnsureSuccessStatusCode();

            var result = await response
                .Content
                .ReadFromJsonAsync<AuthorizationResponse>(JsonOptions.Default);

            return result!.Token;
        }

        [UsedImplicitly]
        private record AuthorizationResponse(string Token);
    }
}