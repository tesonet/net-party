using System.Net.Http.Formatting;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Interfaces.Clients;
using Pathoschild.Http.Client;

namespace NetParty.Clients
{
    public class AuthorizationClient : IAuthorizationClient
    {
        private readonly IClient _client;

        public AuthorizationClient(IClient client)
        {
            _client = client;
        }

        public async Task<string> FetchToken(Credentials credentials)
        {
            var result = await _client.PostAsync("v1/tokens")
                .WithBody(builder => builder.Model(new
                {
                    username = credentials.Username,
                    password = credentials.Password
                }, new JsonMediaTypeFormatter()))
                .WithOptions(true);

            if (!result.IsSuccessStatusCode)
                return null;
            
            return (await result.AsRawJsonObject())["token"].ToString();
        }
    }
}