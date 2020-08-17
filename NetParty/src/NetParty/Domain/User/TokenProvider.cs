using System;
using System.Net.Http;
using System.Threading.Tasks;
using NetParty.Data.Http;

namespace NetParty.Domain.User
{
    public class TokenProvider : ITokenProvider
    {
        private class TokenResponse
        {
            public string Token { get; set; }
        }

        private readonly HttpClient _client;
        private readonly string _url;
        private readonly ICredentialService _credentialService;

        public TokenProvider(HttpClient client, string url, ICredentialService credentialService)
        {
            _client = client;
            _url = url;
            _credentialService = credentialService;
        }

        public async Task<string> GetToken()
        {
            var credentials = await _credentialService.GetAsync();

            if (credentials == null)
            {
                throw new Exception("Application credentials must be configured first.");
            }

            return (await _client.PostAsync<TokenResponse>(credentials, _url)).Token;
        }
    }
}
