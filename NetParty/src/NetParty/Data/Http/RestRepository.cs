using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetParty.Data.Http
{
    internal class RestRepository<TItem> : IReadOnlyRepository<TItem>
    {
        private readonly HttpClient _client;
        private readonly ITokenProvider _tokenProvider;
        private readonly string _url;

        public RestRepository(HttpClient client, ITokenProvider tokenProvider, string url)
        {
            _client = client;
            _tokenProvider = tokenProvider;
            _url = url;
        }

        public async Task<TItem> GetAsync()
        {
            var token = await _tokenProvider.GetToken();

            return await _client.GetAsync<TItem>(_url, new Dictionary<string, string> { { "Authorization", $"Bearer {token}" } });
        }
    }
}
