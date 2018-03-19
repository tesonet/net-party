using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Tesonet
{
    public class TesonetService : ITesonetService
    {
        public Token GetAccessToken(string username, string password)
        {
            var client = new RestClient(Constants.RequestTokenUrl);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"username\"\r\n\r\n" + username + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"password\"\r\n\r\n" + password + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<Token>(response.Content);
        }

        public IList<Server> GetServerList(Token token)
        {
            var client = new RestClient(Constants.RequestServersUrl);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", token.Value);
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<IList<Server>>(response.Content);
        }
    }
}
