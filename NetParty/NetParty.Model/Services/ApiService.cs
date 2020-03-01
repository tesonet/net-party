using NetParty.Common.Log;
using NetParty.Model.Entities;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace NetParty.Model.Services
{
    public class ApiService : IApiService
    {
        public ApiService(ILogProvider logProvider)
        {
        }

        public GenerateTokenResponse GenerateToken(Credentials args)
        {
            var client = new RestClient("http://playground.tesonet.lt/v1/tokens");
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            var serializedBody = JsonConvert.SerializeObject(args);
            request.AddJsonBody(serializedBody);

            var res = ExecuteRequest<GetTokenResponse>(client, request).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return new GenerateTokenResponse { Success = true, Token = res.Data.Token };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ApiErrorResponse>(res.Content);
                return new GenerateTokenResponse { Message = $"Could not get token from remote. {error?.Message}" };
            }
        }

        public GetServersResponse GetServersList(GetApiServersArgs args)
        {
            var client = new RestClient("http://playground.tesonet.lt/v1/servers");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {args.Token}");

            var res = ExecuteRequest<List<Server>>(client, request).Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return new GetServersResponse { Success = true, Servers = res.Data };
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ApiErrorResponse>(res.Content);
                return new GetServersResponse { Message = $"Could not get servers list from remote. {error?.Message}" };
            }
        }

        private async Task<IRestResponse<T>> ExecuteRequest<T>(RestClient client, RestRequest request)
        {
            return await client.ExecuteAsync<T>(request);
        }
    }
}