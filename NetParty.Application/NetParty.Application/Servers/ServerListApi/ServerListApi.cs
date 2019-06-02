#region Using

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pathoschild.Http.Client;

#endregion

namespace NetParty.Application.Servers.ServerListApi
    {
    public class ServerListApi : IServerProvider
        {
        private const string ServersPath = "servers";
        private readonly IClient m_client;
        private readonly IServerListApiTokenProvider m_tokenProvider;

        public ServerListApi(IClient client, IServerListApiTokenProvider tokenProvider)
            {
            m_client = client ?? throw new ArgumentNullException(nameof(client));
            m_tokenProvider = tokenProvider ?? throw new ArgumentNullException(nameof(tokenProvider));
            }

        public async Task<IEnumerable<Server>> GetServersAsync()
            {
            var response = await m_client.GetAsync(ServersPath).WithBearerAuthentication(await Token());
            if (!response.IsSuccessStatusCode)
                throw new ApiException(response, $"Error while accessing {nameof(ServerListApi)} api");

            return await response.As<IEnumerable<Server>>();
            }

        private Task<string> Token() => m_tokenProvider.GetTokenAsync();
        }
    }
