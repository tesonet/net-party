using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;

namespace partycli.WebApiClient
{
    public class WebApiClient
    {
        HttpClient client = new HttpClient();

        public async Task<HttpResponseMessage> Get(string uri)
        {
            return await client.GetAsync(uri);
        }

        public async Task<string> GetToken()
        {
            string username = "tesonet";
            string password = "partyanimal";
            string url = "http://playground.tesonet.lt/v1/tokens";
            string token = string.Empty;
            var jsonBody = JsonConvert.SerializeObject(new { username, password });

            using (HttpClient client = new HttpClient())
            using (var content = new StringContent(jsonBody, Encoding.UTF8, "application/json"))
            {
                var response = await client.PostAsync(url, content);
                
                response.EnsureSuccessStatusCode();

                var jsonString = await response.Content.ReadAsStringAsync();

                token = JsonConvert.DeserializeObject<string>(jsonString);
            }
            
            return token;
        }
    }
}
