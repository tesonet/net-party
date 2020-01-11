using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using net_party.Common.Serializers;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace net_party.Services
{
    public abstract class BaseRestService
    {
        protected readonly IServiceProvider _services;
        protected readonly ILogger _logger;
        protected readonly IConfigurationRoot _config;

        protected BaseRestService(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
            _logger = _services.GetService<ILoggerFactory>().CreateLogger(this.GetType());
            _config = serviceProvider.GetService<IConfigurationRoot>();
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
            var uri = new Uri(_config.GetSection("Server:BaseUrl").Value);
            var client = new RestClient(uri);
            client.UseSerializer(() => new JsonNetSerializer());

            return client;
        }

        protected async Task<T> ExecuteAsync<T>(IRestRequest request)
        {
            var response = await BaseClient().ExecuteAsync(request, new CancellationToken());

            return new JsonDeserializer().Deserialize<T>(response);
        }
    }
}