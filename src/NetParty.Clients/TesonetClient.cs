using GuardNet;
using NetParty.Clients.Interfaces;
using NetParty.Contracts;
using Pathoschild.Http.Client;
using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace NetParty.Clients
{
    public class TesonetClient : ITesonetClient
    {
        private readonly IClient _client;

        public TesonetClient(string connectionString)
        {
            Guard.NotNull(connectionString, nameof(connectionString));

            _client = new FluentClient(connectionString);
        }

        public async Task<ServerDto[]> GetServersAsync(string token)
        {
            ServerDto[] response = await _client.GetAsync("v1/servers")
                .WithBearerAuthentication(token)
                .WithOptions(true)
                .AsArray<ServerDto>();

            return response;
        }

        public async Task<string> GetTokenAsync(string userName, string password)
        {
            Guard.NotNullOrEmpty(userName, nameof(userName), "Username is required field");
            Guard.NotNullOrEmpty(password, nameof(password), "Password is required field");

            var requestData = new
            {
                username = userName,
                password = password
            };

            IResponse response = await _client.PostAsync("v1/tokens")
               .WithBody(b => b.Model(requestData, new JsonMediaTypeFormatter()))
               .WithOptions(true);

            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Message.ReasonPhrase);

            var jObject = await response.AsRawJsonObject();

            return jObject["token"].ToString();
        }
    }
}
