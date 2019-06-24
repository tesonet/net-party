using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetPartyCore.Datastore.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;

namespace NetPartyCore.Network
{
    class RemoteApi : IRemoteApi
    {

        private static HttpClient httpClient = new HttpClient();

        public RemoteApi()
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri("http://playground.tesonet.lt/v1/");
        }

        public async Task<string> GetToken(Client client)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "username", client.Username },
                { "password", client.Password }
            });

            httpClient.DefaultRequestHeaders.Authorization = null;
            var response = await httpClient.PostAsync("tokens", content);

            // will throw an exception if status code is not 200
            response.EnsureSuccessStatusCode();

            var jsonsteam = await response.Content.ReadAsStreamAsync();
            var serializer = new DataContractJsonSerializer(typeof(TokenResponse));
            var tokenResponse = (TokenResponse)serializer.ReadObject(jsonsteam);
            return tokenResponse.token;
        }

        public async Task<List<Server>> GetServers(string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.GetAsync("servers");
            // will throw an exception if status code is not 200
            response.EnsureSuccessStatusCode();

            var jsonsteam = await response.Content.ReadAsStreamAsync();
            var serializer = new DataContractJsonSerializer(typeof(List<ServersResponse>));
            var serversResponse = (List<ServersResponse>)serializer.ReadObject(jsonsteam);
            var servers = new List<Server>();
            serversResponse.ForEach(server => {
                servers.Add(new Server(0, server.name, server.distance));
            });
            return servers;
        }
    }
}