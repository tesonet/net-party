using PartyCli.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using PartyCli.Core.Entities;
using PartyCli.Infrastructure.ServersApis;

namespace PartyCli.IntegrationTests
{
    public class WebServersApiTests
    {
        [Fact]
        public void WebServersApi_IsGettingServers()
        {
            var mokApiAuthCredentialsRepository  = new Mock<IApiAuthCredentialsRepository>();
            var mokLogger = new Mock<ILogger>();
            var mokConfiguration = new Mock<IConfiguration>();

            var allCredentials = new List<ApiAuthCrediancials>()
            {
                new ApiAuthCrediancials() {UserName = "tesonet", Password = "partyanimal"}
            };

            mokApiAuthCredentialsRepository
                .Setup(x => x.GetAll())
                .Returns(() => allCredentials);

  
            mokConfiguration
                .Setup(x => x["AuthApiUrl"])
                .Returns(() => "http://playground.tesonet.lt/v1/tokens");

            mokConfiguration
                .Setup(x => x["ServersApiUrl"])
                .Returns(() => "http://playground.tesonet.lt/v1/servers");

            var webServersApi = new WebServersApi(mokApiAuthCredentialsRepository.Object,
                                                  mokLogger.Object,
                                                  mokConfiguration.Object
                                                 );

            var result = webServersApi.GetServersAsync().Result;

            Assert.Equal(true, result.Any());
        }
    }
}
