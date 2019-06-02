#region Using

using System;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using NetParty.Application.CredentialsNS;
using Pathoschild.Http.Client;

#endregion

namespace NetParty.Application.Servers.ServerListApi
    {
    public class ServerListApiTokenProvider : IServerListApiTokenProvider
        {
        private const string TokenProviderPath = "tokens";
        private readonly IClient m_client;
        private readonly ICredentialsRepository m_credentialsRepository;

        public ServerListApiTokenProvider(IClient client, ICredentialsRepository credentialsRepository)
            {
            m_client = client ?? throw new ArgumentNullException(nameof(client));
            m_credentialsRepository = credentialsRepository ?? throw new ArgumentNullException(nameof(credentialsRepository));
            }

        public async Task<string> GetTokenAsync()
            {
            var credentials = await Credentials();
            var response = await m_client.PostAsync(TokenProviderPath)
                .WithBody(builder => builder.Model(credentials, new JsonMediaTypeFormatter()))
                .WithOptions(true);

            if (!response.IsSuccessStatusCode)
                throw new ApiException(response, $"Error while accessing {nameof(ServerListApiTokenProvider)} api");

            var resultObject = await response.AsRawJsonObject();
            return resultObject["token"].ToString();
            }

        private async Task<object> Credentials()
            {
            var credentials = await m_credentialsRepository.LoadAsync();
            return new
                {
                username = credentials.Username,
                password = credentials.Password
                };
            }
        }
    }
