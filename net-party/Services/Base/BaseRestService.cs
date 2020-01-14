using net_party.Common.Serializers;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace net_party.Services.Base
{
    public abstract class BaseRestService : BaseService
    {
        private const string BASE_URL = "http://playground.tesonet.lt/v1";

        protected BaseRestService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected static RestRequest BaseRequest(string url, Method method)
        {
            var request = new RestRequest(url, method)
            {
                RequestFormat = DataFormat.Json,
            };

            return request;
        }

        protected RestClient BaseClient()
        {
            var uri = new Uri(BASE_URL);
            var client = new RestClient(uri);
            client.UseSerializer(() => new JsonNetSerializer());

            return client;
        }

        protected async Task<T> ExecuteAsync<T>(IRestRequest request)
        {
            var response = await BaseClient().ExecuteAsync(request, new CancellationToken());

            if (response.IsSuccessful)
                return new JsonDeserializer().Deserialize<T>(response);

            Console.WriteLine("Command failed.");

            return default;
        }

        protected async Task<IEnumerable<T>> ExecuteManyAsync<T>(IRestRequest request)
        {
            var response = await BaseClient().ExecuteAsync(request, new CancellationToken());

            return new JsonDeserializer().Deserialize<IEnumerable<T>>(response);
        }
    }
}