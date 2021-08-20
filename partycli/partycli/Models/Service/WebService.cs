
using Newtonsoft.Json;
using partycli.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Models.Service
{

    public class WebService : IWebService
    {
        public WebService()
        {
        }
        public GenerateTokenResponse GenerateToken(Credentials args)
        {
            //System.Diagnostics.Debugger.Launch();
            var url = "http://playground.tesonet.lt/v1/tokens";
            var client = new RestClient(url);

         
            var serializedBody = JsonConvert.SerializeObject(args);

            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter(null, serializedBody, ParameterType.RequestBody);
            var res = ExecuteAsPost<Tokens>(client, request).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return new GenerateTokenResponse { Success = true, Token = res.Data.token };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<WebServiceErrorResponse>(res.Content);
                return new GenerateTokenResponse { Message = $"Getting token error {error?.Message}" };
            }
        }

        public GetServersListResponse GetServersList(GetWebServerServerListArgs args)
        {
            var client = new RestClient("http://playground.tesonet.lt/v1/servers");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {args.Token}");

            var res = ExecuteRequest<List<ServerList>>(client, request).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return new GetServersListResponse { Success = true, Servers = res.Data };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<WebServiceErrorResponse>(res.Content);
                return new GetServersListResponse { Message = $"Getting server list error. {error?.Message}" };
            }
        }

        private async Task<IRestResponse<T>> ExecuteRequest<T>(RestClient client, RestRequest request)
        {
            return await client.ExecuteAsync<T>(request);
        }
        private async Task<IRestResponse<T>> ExecuteAsPost<T>(RestClient client, RestRequest request)
        {
              return await client.ExecutePostAsync<T>(request);
        }
    }
}
