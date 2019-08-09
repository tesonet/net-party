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
        ILog m_log = null;
        public HttpService(string url, ILog log)
        {
            m_url = url;
            m_log = log;
        }
        public async Task<IRequestResult<string>> GetWithToken(string token)
        {
            dynamic httpResponse = await m_url
                    .WithHeader("Accept", "application/json")
                    .WithHeader("Authorization", "Bearer " + token)
                    .GetJsonListAsync();
            
            return new SuccessResult<string>(JsonConvert.SerializeObject(httpResponse));
        }

        public async Task<IRequestResult<string>> PostJson(string content)
        {
            dynamic httpResponse = await m_url
                .WithHeader("Content-Type", "application/json")
                .PostAsync(new StringContent(content))
                .ReceiveJson();

            if (httpResponse.token != null)
                return new SuccessResult<string>(httpResponse.token as string);
            return new FailedResult(httpResponse.message as string);
            
        }
    }
}
