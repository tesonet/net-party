using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PartyCli.Interfaces;
using PartyCli.Models;
using PartyCli.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PartyCli.Api
{
    public class ServerApi : IServerApi
    {
        private ServerApiOptions options;
        private readonly HttpClient client = new HttpClient();

        public ServerApi(IOptions<ServerApiOptions> options)
        {
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task AuthorizeAsync(Credentials credentials)
        {
            var data = new
            {
                username = credentials.Username,
                password = credentials.Password
            };

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await client.PostAsync(options.TokensUri, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Authorization request has failed! Status code: {response.StatusCode.ToString()}.");
            }

            string json = await response.Content.ReadAsStringAsync();

            var apiToken = new { token = "" };
            apiToken = JsonConvert.DeserializeAnonymousType(json, apiToken);

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiToken.token);
        }

        public async Task<IEnumerable<Server>> GetServersAsync()
        {
            var response = await client.GetAsync(options.ServersUri);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Get servers request has failed! Status code: {response.StatusCode.ToString()}.");
            }

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<Server>>(json); 
        }
    }
}
