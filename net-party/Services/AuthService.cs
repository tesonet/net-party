using net_party.Entities.API;
using net_party.Entities.Database;
using net_party.Services.Contracts;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace net_party.Services
{
    public class AuthService : BaseRestService, IAuthService
    {
        #region Endpoints

        private const string SERVER_LIST = "/servers";
        private const string TOKEN = "/tokens";

        #endregion Endpoints

        public AuthService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<AuthToken> AcquireNewToken()
        {
            var username = _config["Server:Username"];
            var password = _config["Server:Password"];
            var request = BaseRequest(TOKEN, Method.POST);
            request.AddJsonBody(TokenRequest.New(username, password));

            var response = await ExecuteAsync<TokenResponse>(request);

            return AuthToken.FromTokenResponse(response);
        }
    }
}