using NetParty.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using System.Net.Http.Headers;

namespace NetParty.Api
{
    public class PlaygroundApi: IApi
    {
        private IConfiguration _configuration;
        private readonly HttpClient _client;
        public PlaygroundApi()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            ;
            _client = new HttpClient();
        }

        public async Task<string> GetAuthorizationToken(string username, string password)
        {
            var uri = new Uri(_configuration.GetSection("authorization_url").Value);
            var request = JsonConvert.SerializeObject(new { username = username, password = password });
            var remoteResult = await _client.PostAsync(uri, new StringContent(request, Encoding.UTF8, "application/json"));
            var contentOfResponse = await remoteResult.Content.ReadAsStringAsync();
            var jsonResult = JsonConvert.DeserializeObject<Token>(contentOfResponse);
            string token = jsonResult.token;

            return token;
        }
         
        public async Task<IList<IServer>> GetServers(string token)
        {
            var uri = new Uri(_configuration.GetSection("servers_list_url").Value);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var remoteResult = await _client.GetAsync(uri);
            var contentOfResponse = await remoteResult.Content.ReadAsStringAsync();
            var res = JsonConvert.DeserializeObject<Server[]>(contentOfResponse);

            return res;
        }
    }
}
