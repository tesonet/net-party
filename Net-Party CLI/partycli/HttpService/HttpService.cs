using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Newtonsoft.Json;
using log4net;
using partycli.Helpers;
using System.Dynamic;
using System.Collections.Generic;

namespace partycli.Http
{
    public class HttpService : IHttpService
    {
        string m_url = null;
        public HttpService(string url)
        {
            m_url = url;
        }
        public async Task<IRequestResult<string>> GetWithToken(string token)
        {
            dynamic httpResponse = await m_url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("Authorization", "Bearer " + token)
                    .GetJsonListAsync();
            return getResponse(httpResponse);
        }

        public async Task<IRequestResult<string>> PostJson(string serializedCredentials)
        {
            dynamic httpResponse = await m_url
                .WithHeader("Content-Type", "application/json")
                .PostAsync(new StringContent(serializedCredentials))
                .ReceiveJson();
            return getResponse(httpResponse);
        }

        private static IRequestResult<string> getResponse(dynamic httpResponse)
        {
            if (IsPropertyExist(httpResponse,"message"))
                return new FailedResult(httpResponse.message as string);
            if (IsPropertyExist(httpResponse, "token"))
                return new SuccessResult<string>(httpResponse.token as string);
            return new SuccessResult<string>(JsonConvert.SerializeObject(httpResponse));
        }

        private static bool IsPropertyExist(dynamic settings, string name)
        {
            if (settings is ExpandoObject)
                return ((IDictionary<string, object>)settings).ContainsKey(name);

            return settings.GetType().GetProperty(name) != null;
        }
    }
}
