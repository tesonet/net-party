using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace PartyCLI
{
    public class ServerRequestsManager : IServerRequestsManager
    {
        private readonly HttpClient client = new HttpClient();

        public async Task<AccessToken> GetTokenAsync(UserData userCredentials)
        {
            var authJson = JsonConvert.SerializeObject(userCredentials).ToLower();

            var requestContent = new StringContent(authJson, Encoding.UTF8, "application/json");
            var requestResponse = await client.PostAsync(Properties.Settings.Default.TokenUrl, requestContent);

            if (!requestResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Error while getting token: " + requestResponse.StatusCode);
                Environment.Exit(0);
            }

            var responseString = await requestResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<AccessToken>(responseString);
            return result;
        }

        public async Task<List<Server>> GetServersAsync(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var requestResponse = await client.GetAsync(Properties.Settings.Default.ServersListUrl);

            if (!requestResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("Error while getting servers list: " + requestResponse.StatusCode);
                Environment.Exit(0);
            }

            var responseString = await requestResponse.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<List<Server>>(responseString);
            return result;
        }
    }
}
