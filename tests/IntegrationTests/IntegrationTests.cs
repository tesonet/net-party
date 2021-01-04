namespace TesonetDotNetParty.IntegrationTests
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    [Collection("integration")]
    public class IntegrationTests
    {
        [Fact]
        public async Task ShouldSetConfigurationFileToProvidedValues()
        {
            var args = "config --username user --password pass".Split(' ');
            await using var application = new CommandLineApplication(args);
            var returnCode = await application.RunAsync();

            Assert.Equal(0, returnCode);

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            Assert.Equal("user", config.GetValue<string>("ServerListApi:Username"));
            Assert.Equal("pass", config.GetValue<string>("ServerListApi:Password"));
            Assert.Equal("https://playground.tesonet.lt/v1/", config.GetValue<string>("ServerListApi:BaseAddress"));
        }
    }
}