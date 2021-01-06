namespace TesonetDotNetParty.IntegrationTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using FakeItEasy;
    using Microsoft.Extensions.Configuration;
    using Tesonet.ServerListApp.Domain;
    using Tesonet.ServerListApp.Infrastructure.Configuration;
    using Tesonet.ServerListApp.Infrastructure.ServerListApi;
    using Tesonet.ServerListApp.Infrastructure.Storage;
    using Xunit;

    [Collection("integration")]
    public class ApplicationTests
    {
        [Fact]
        public async Task SetsConfigurationFileToProvidedValues()
        {
            var args = "config --username user --password pass".Split(' ');
            await using var application = new CommandLineApplication(args);
            var returnCode = await application.RunAsync();

            Assert.Equal(0, returnCode);

            var config = new ServersListApiConfig();
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build()
                .GetSection("ServerListApi")
                .Bind(config);

            Assert.Equal("user", config.Username);
            Assert.Equal("pass", config.Password);
            Assert.Equal("https://playground.tesonet.lt/v1/", config.BaseAddress);
        }

        [Fact]
        public async Task SavesServerListToPersistentStorage()
        {
            await new PersistentJsonConfiguration().Save(new
            {
                ServerListApi = new
                {
                    Username = "user",
                    Password = "pass",
                    BaseAddress = "https://host/"
                }
            });

            var expectedServers = new[]
            {
                new Server(Guid.NewGuid(), "Lithuania #1", 20),
                new Server(Guid.NewGuid(), "Latvia #1", 500),
                new Server(Guid.NewGuid(), "Germany #1", 1234),
                new Server(Guid.NewGuid(), "United Kingdom #1", 2500)
            };

            await using var application = new CommandLineApplication("server_list");
            var handler = application.FakeHttpClientHandler;

            var callToAuthorize = A.CallTo(() => handler.SendAsyncOverride(
                A<HttpRequestMessage>.That.Matches(message =>
                    message.Method == HttpMethod.Post
                    && message.RequestUri!.ToString().EndsWith("tokens")),
                A<CancellationToken>._));

            var callToGetServerList = A.CallTo(() => handler.SendAsyncOverride(
                A<HttpRequestMessage>.That.Matches(message =>
                    message.Method == HttpMethod.Get
                    && message.RequestUri!.ToString().EndsWith("servers")
                    && message.Headers.Authorization!.Scheme == "Bearer"
                    && message.Headers.Authorization!.Parameter == "1234567890"),
                A<CancellationToken>._));

            callToAuthorize.Returns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(@"{""token"":""1234567890""}")
            });

            callToGetServerList.Returns(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(expectedServers))
            });

            var returnCode = await application.RunAsync();

            await using var dbContext = new ServersDbContext();
            foreach (var (id, name, distance) in expectedServers)
            {
                var server = await dbContext.FindAsync<Server>(id);
                Assert.Equal(name, server.Name);
                Assert.Equal(distance, server.Distance);
            }

            callToAuthorize.MustHaveHappenedOnceExactly();
            callToGetServerList.MustHaveHappenedOnceExactly();

            Assert.Equal(0, returnCode);
        }

        [Fact]
        public async Task LoadsServerListFromPersistentStorage()
        {
            var expectedServers = new[]
            {
                new Server(Guid.NewGuid(), "Lithuania #1", 20),
                new Server(Guid.NewGuid(), "Latvia #1", 500),
                new Server(Guid.NewGuid(), "Germany #1", 1234),
                new Server(Guid.NewGuid(), "United Kingdom #1", 2500)
            };

            await using var application = new CommandLineApplication("server_list", "--local");
            var handler = application.FakeHttpClientHandler;

            var callToAuthorize = A.CallTo(() => handler.SendAsyncOverride(
                A<HttpRequestMessage>.That.Matches(message =>
                    message.Method == HttpMethod.Post
                    && message.RequestUri!.ToString().EndsWith("tokens")),
                A<CancellationToken>._));

            var callToGetServerList = A.CallTo(() => handler.SendAsyncOverride(
                A<HttpRequestMessage>.That.Matches(message =>
                    message.Method == HttpMethod.Get
                    && message.RequestUri!.ToString().EndsWith("servers")),
                A<CancellationToken>._));

            var returnCode = await application.RunAsync(async dbContext =>
            {
                await dbContext.Servers.AddRangeAsync(expectedServers);
                await dbContext.SaveChangesAsync();
            });

            callToAuthorize.MustNotHaveHappened();
            callToGetServerList.MustNotHaveHappened();

            Assert.Equal(0, returnCode);
        }
    }
}