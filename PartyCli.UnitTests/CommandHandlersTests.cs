using Moq;
using PartyCli.Core.Entities;
using PartyCli.Core.Enums;
using PartyCli.Core.Interfaces;
using PartyCli.Infrastructure;
using PartyCli.Infrastructure.ComamndHandlers;
using PartyCli.Infrastructure.Exeptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PartyCli.UnitTests
{
    public class CommandHandlersTests
    {
        [Fact]
        public void ConfigCommandHandler_IsHandling()
        {
            var mockApiAuthCredentialsRepository = new Mock<IApiAuthCredentialsRepository>();
            var mockPresenter = new Mock<IPresenter>();
            var mockLogger = new Mock<ILogger>();

            ApiAuthCrediancials resultEntity = null;
            string displayMessage = "";             

            mockApiAuthCredentialsRepository
                .Setup(x => x.Add(It.IsAny<ApiAuthCrediancials>()))
                .Callback<ApiAuthCrediancials>(c => resultEntity = c);

            mockPresenter
                .Setup(x => x.DisplayMessage(It.IsAny<string>()))
                .Callback<string>(m => displayMessage = m);

            var args = new string[] { "config", "--username", "YOUR USERNAME", "--password", "YOUR PASSWORD" };

            var configCommandHandler = new ConfigCommandHandler(mockApiAuthCredentialsRepository.Object,
                                                                mockPresenter.Object,
                                                                mockLogger.Object);

            configCommandHandler.Handle(args);

            Assert.Equal("YOUR USERNAME", resultEntity.UserName);
            Assert.Equal("YOUR PASSWORD", resultEntity.Password);
            Assert.Equal("Configuration saved successfully.", displayMessage);

        }

        [Theory]
        [InlineData("config", "--user", "YOUR USERNAME", "--password", "YOUR PASSWORD")]
        [InlineData("config", "--username", "YOUR USERNAME", "--password2", "YOUR PASSWORD")]
        [InlineData("config", "--username", "YOUR USERNAME", "--password")]
        [InlineData("config", "--username", "YOUR USERNAME", "--password", "YOUR PASSWORD", "one more")]
        public void ConfigCommandHandler_ThrowsIfBadParams(params string[] args )
        {
            var mockApiAuthCredentialsRepository = new Mock<IApiAuthCredentialsRepository>();
            var mockPresenter = new Mock<IPresenter>();
            var mockLogger = new Mock<ILogger>();
    
            var configCommandHandler = new ConfigCommandHandler(mockApiAuthCredentialsRepository.Object,
                                                                mockPresenter.Object,
                                                                mockLogger.Object);

            Exception ex = Assert.Throws<PresentableExeption>(() => configCommandHandler.Handle(args));

            Assert.Equal("Invalid arguments.", ex.Message);
        }

        [Fact]
        public void ServerListCommandHandler_IsHandling()
        {
            var mockServersRepository = new Mock<IServersRepository>();
            var mockPresenter = new Mock<IPresenter>();
            var mockLogger = new Mock<ILogger>();
            var mockServersApi = new Mock<IServersApi>();

            IEnumerable<Server> resultEntities = null;
            List<Server> displayServers = null;

            List<Server> serversList = new List<Server>()
            {
                new Server(){ Name = "Germany #79", Distance = 157 },
                new Server(){ Name = "United Kingdom #26", Distance = 1360 }
            };

            mockServersApi
                .Setup(x => x.GetServersAsync())
                .Returns( () => Task.FromResult(serversList));

            mockServersRepository
                .Setup(x => x.AddRange(It.IsAny<IEnumerable<Server>>()))
                .Callback<IEnumerable<Server>>(c => resultEntities = c);

            mockPresenter
                .Setup(x => x.DisplayServers(It.IsAny<List<Server>>()))
                .Callback<List<Server>>(m => displayServers = m);

            var args = new string[] { "server_list"};

            var configCommandHandler = new ServerListCommandHandler(mockServersApi.Object,
                                                                    mockServersRepository.Object,
                                                                    mockPresenter.Object,
                                                                    mockLogger.Object);

            configCommandHandler.Handle(args);

            Assert.Equal(serversList, resultEntities);
            Assert.Equal(serversList, displayServers);       

        }


    }
}
