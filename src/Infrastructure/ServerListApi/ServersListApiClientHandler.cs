namespace Tesonet.ServerListApp.Infrastructure.ServerListApi
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Net.Mime;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Http;
    using JetBrains.Annotations;
    using Microsoft.Extensions.Logging;
    using static ServersListApiConfig;

    internal sealed class ServersListApiClientHandler : LoggingHttpClientHandler
    {
        public delegate Task<HttpResponseMessage> SendAsyncDelegate(
            HttpRequestMessage message,
            CancellationToken ct);

        private readonly Credentials _credentials;
        private readonly SendAsyncDelegate? _authDelegate;
        private readonly SendAsyncDelegate? _serverListDelegate;

        public ServersListApiClientHandler(
            Credentials credentials,
            ILogger logger)
            : base(logger)
        {
            _credentials = credentials;
        }

        public ServersListApiClientHandler(
            Credentials credentials,
            SendAsyncDelegate authDelegate,
            SendAsyncDelegate serverListDelegate,
            ILogger logger)
            : base(logger)
        {
            _credentials = credentials;
            _authDelegate = authDelegate;
            _serverListDelegate = serverListDelegate;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken ct)
        {
            var baseAddress = request
                .RequestUri
                !.GetLeftPart(UriPartial.Authority);

            var token = await Authorize(baseAddress, ct);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return _serverListDelegate != null
                ? await _serverListDelegate(request, ct)
                : await base.SendAsync(request, ct);
        }

        private async Task<string> Authorize(string baseAddress, CancellationToken ct)
        {
            using var content = new StringContent(
                JsonSerializer.Serialize(_credentials, JsonOptions.Default),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            using var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(new Uri(baseAddress), "v1/tokens"),
                Content = content
            };

            using var response = _authDelegate != null
                ? await _authDelegate(requestMessage, ct)
                : await base.SendAsync(requestMessage, ct);

            response.EnsureSuccessStatusCode();

            var result = await response
                .Content
                .ReadFromJsonAsync<AuthorizationResponse>(JsonOptions.Default, ct);

            return result!.Token;
        }

        [UsedImplicitly]
        private record AuthorizationResponse(string Token);
    }
}