namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using Http;
    using Microsoft.Extensions.Logging;

    internal class ServerListApiClientHandler : LoggingHttpClientHandler
    {
        private readonly IAuthClient _authClient;

        public ServerListApiClientHandler(IAuthClient authClient, ILogger logger) : base(logger)
        {
            _authClient = authClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken ct)
        {
            var token = await _authClient.Authorize();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, ct);
        }
    }
}