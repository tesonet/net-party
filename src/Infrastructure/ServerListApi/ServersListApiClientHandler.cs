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

    internal class ServersListApiClientHandler : LoggingHttpClientHandler
    {
        private readonly Credentials _credentials;

        public ServersListApiClientHandler(Credentials credentials, ILogger logger)
            : base(logger)
        {
            _credentials = credentials;
        }

        public virtual Task<HttpResponseMessage> SendAsyncOverride(
            HttpRequestMessage message,
            CancellationToken ct)
        {
            return base.SendAsync(message, ct);
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken ct)
        {
            var baseAddress = request.RequestUri!.GetLeftPart(UriPartial.Authority);

            var token = await Authorize(baseAddress, ct);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await SendAsyncOverride(request, ct);
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

            using var response = await SendAsyncOverride(requestMessage, ct);
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