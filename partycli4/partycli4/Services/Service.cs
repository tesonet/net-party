using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using partycli4.Data;
using partycli4.Interface;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace partycli4.Services
{
    public class Service : IProvideData
    {
        public string GetGeneratedToken(User user)
        {
            using var client = new HttpClient();
            var authJson = JsonConvert.SerializeObject(user).ToLower();
            using var http = new StringContent(authJson, Encoding.UTF8, "application/json");
            var postResult = client.PostAsync(ConfigurationManager.AppSettings["GetGeneratedToken"], http).Result;
            postResult.EnsureSuccessStatusCode();
            var result = postResult.Content.ReadAsStringAsync().Result;
            var accessToken = JsonConvert.DeserializeObject<GeneratedToken>(result);
            return accessToken.Token;
        }

        public IList<Server> GetServerList(String accessToken)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var postResult = client.GetAsync(ConfigurationManager.AppSettings["GetServiceList"]).Result;
            postResult.EnsureSuccessStatusCode();
            var result = postResult.Content.ReadAsStringAsync().Result;
            var list = JsonConvert.DeserializeObject<List<Server>>(result);
            return list;
        }

    }
}
