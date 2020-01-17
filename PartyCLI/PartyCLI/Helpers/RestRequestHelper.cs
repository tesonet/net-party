namespace PartyCLI.Helpers
{
    using System.Collections.Generic;

    using RestSharp;

    public static class RestRequestHelper
    {
        public static RestRequest CreateRequest(string resource, Dictionary<string, string> parameters, Dictionary<string, string> headers)
        {
            var request = new RestRequest(resource) { RequestFormat = DataFormat.Json };

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    request.AddParameter(param.Key, param.Value);
                }
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            return request;
        }
    }
}