using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPartyCore.Controller;
using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Framework;
using NetPartyCore.Network;
using NetPartyCore.Output;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetPartyCore.Exception;

namespace NetPartyTest.Controller
{
    [TestClass]
    public class ServerControllerTest
    {
        private Mock<IRemoteApi> remoteApiMock;
        private Mock<IStorage> storageMock;
        private Mock<IOutputFormatter> outputMock;
        private IServiceProvider serviceProvider;

        private List<Server> localServerList;
        private List<ServersResponse> remoteServerList;

        [TestInitialize]
        public void PrepareForTest()
        {
            remoteApiMock = new Mock<IRemoteApi>();
            storageMock = new Mock<IStorage>();
            outputMock = new Mock<IOutputFormatter>();

            serviceProvider = new ServiceCollection()
                .AddSingleton<IRemoteApi>((_) => remoteApiMock.Object)
                .AddSingleton<IStorage>((_) => storageMock.Object)
                .AddSingleton<IOutputFormatter>((_) => outputMock.Object)
                .BuildServiceProvider();

            localServerList = new List<Server>()
            {
                new Server()
                {
                    Name = "Local Server",
                    Distance = 1
                }
            };

            remoteServerList = new List<ServersResponse>()
            {
                new ServersResponse()
                {
                    name = "Remote Server",
                    distance = 1
                }
            };
        }

        [TestMethod]
        public async Task LocalServerListActionTest()
        {
            storageMock.Setup(m => m.GetServers()).Returns(localServerList);

            await CoreController
                .CreateWithProvider<ServerController>(serviceProvider)
                .ServerListAction(true);
            
            outputMock.Verify(mock => mock.PrintServers(
                It.Is<List<Server>>(list => list == localServerList)
            ));
        }

        [TestMethod]
        public async Task RemoteServerListActionTest()
        {
            storageMock.Setup(mock => mock.GetConfiguration()).Returns(new Client() {
                Username = "api-username",
                Password = "api-password"
            });

            remoteApiMock.Setup(mock => mock.GetToken("api-username", "api-password")).Returns(
                Task.FromResult(new TokenResponse() {
                    token = "token-string"
                })
            );

            remoteApiMock.Setup(mock => mock.GetServers("Bearer token-string")).Returns(
                Task.FromResult(remoteServerList)
            );

            storageMock.Setup(mock => mock.GetServers()).Returns(localServerList);

            await CoreController
                .CreateWithProvider<ServerController>(serviceProvider)
                .ServerListAction(false);

            storageMock.Verify(mock => mock.SetSevers(
                It.Is<List<Server>>(list => list.Count == 1)
            ));

            outputMock.Verify(mock => mock.PrintServers(
                 It.Is<List<Server>>(list => list == localServerList)
            ));
        }

        [TestMethod]
        [ExpectedException(typeof(ConfigurationNotFoundException))]
        public async Task TokenNotSetTest()
        {
            await CoreController
                .CreateWithProvider<ServerController>(serviceProvider)
                .ServerListAction(false);
        }

        [TestMethod]
        [ExpectedException(typeof(TokenRetrievalException))]
        public async Task TokenInvalidTest()
        {
            storageMock.Setup(mock => mock.GetConfiguration()).Returns(new Client() {
                Username = "api-username",
                Password = "api-password"
            });

            remoteApiMock.Setup(mock => mock.GetToken("api-username", "api-password")).Throws(new System.Exception());

            await CoreController
                .CreateWithProvider<ServerController>(serviceProvider)
                .ServerListAction(false);
        }

        [TestMethod]
        [ExpectedException(typeof(ServerListRetrievalException))]
        public async Task ServerListFailureTest()
        {
            storageMock.Setup(mock => mock.GetConfiguration()).Returns(new Client() {
                Username = "api-username",
                Password = "api-password"
            });

            remoteApiMock.Setup(mock => mock.GetToken("api-username", "api-password")).Returns(
                Task.FromResult(new TokenResponse() {
                    token = "token-string"
                })
            );

            remoteApiMock.Setup(mock => mock.GetServers("Bearer token-string")).Throws(new System.Exception());

            await CoreController
                .CreateWithProvider<ServerController>(serviceProvider)
                .ServerListAction(false);
        }

    }
}
