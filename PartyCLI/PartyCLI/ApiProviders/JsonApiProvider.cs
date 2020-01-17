namespace PartyCLI.ApiProviders
{
    using System;
    using System.Collections.Generic;

    using PartyCLI.ApiConfigurations;
    using PartyCLI.Helpers;

    using RestSharp;

    public class JsonApiProvider : ApiProvider
    {
        private readonly IRestClient restClient;

        public JsonApiProvider(IRestClient restClient, IApiConfiguration configuration)
        {
            this.restClient = restClient;
            this.restClient.BaseUrl = new Uri(configuration.Url);
        }

        public override T Get<T>(string resource, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null) 
        {
            var request = RestRequestHelper.CreateRequest(resource, parameters, headers);

            return restClient.Get<T>(request).Data;
        }

        public override T Post<T>(string resource, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null)
        {
            var request = RestRequestHelper.CreateRequest(resource, parameters, headers);

            return restClient.Post<T>(request).Data;
        }
    }
}