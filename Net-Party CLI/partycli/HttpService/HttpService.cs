using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;

namespace partycli.Http
{
    public class HttpService : IHttpService
    {
        string m_url = null;
        public HttpService(string url)
        {
            m_url = url;
        }
        public async Task<string> GetWithToken(string token)
        {
            dynamic httpResponse = await m_url
                .WithHeader("Accept", "application/json")
                .WithHeader("Authorization", "Bearer " + token)
                .GetJsonListAsync();

            return JsonConvert.SerializeObject(httpResponse);
        }

        public async Task<dynamic> PostJson(string content)
        {
            dynamic httpResponse = await m_url
                .WithHeader("Content-Type", "application/json")
                .PostAsync(new StringContent(content))
                .ReceiveJson();

            return httpResponse;
        }
    }
}
