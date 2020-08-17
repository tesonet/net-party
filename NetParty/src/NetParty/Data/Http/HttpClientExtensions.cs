using System;
using log4net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetParty.Data.Http
{
    public static class HttpClientExtensions
    {
        private static readonly ILog Log = LogManager.GetLogger(nameof(HttpClientExtensions));

        public static async Task<TResponse> PostAsync<TResponse>(this HttpClient client, object content, string url)
        {
            var contentStr = JsonConvert.SerializeObject(content);
            using (var body = new StringContent(contentStr, Encoding.UTF8, "application/json"))
            {
                Log.Debug($"Executing 'POST {contentStr}' to '{url}'");

                return await HandleResponse<TResponse>(client.PostAsync(url, body), HttpMethod.Post, url);
            }
        }

        public static async Task<TResponse> GetAsync<TResponse>(this HttpClient client, string url, IDictionary<string, string> headers)
        {
            using (var msg = new HttpRequestMessage(HttpMethod.Get, url))
            {
                foreach (var headerName in headers.Keys)
                {
                    msg.Headers.Add(headerName, headers[headerName]);
                }

                Log.Debug($"Sending 'GET {url}'");

                return await HandleResponse<TResponse>(client.SendAsync(msg), HttpMethod.Get, url);
            }
        }

        private static async Task<TResponse> HandleResponse<TResponse>(Task<HttpResponseMessage> responseTask, HttpMethod method, string url)
        {
            var response = await responseTask;

            if (response.Content != null) { 
                var responseBody = await response.Content.ReadAsStringAsync();

                response.Content.Dispose();

                Log.Debug($"Received '{response.StatusCode} {responseBody}' from '{method} {url}'");

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TResponse>(responseBody);
                }
            }

            throw new Exception("Couldn't load data from external source.");
        }
    }
}
