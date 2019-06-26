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
            
            outputMock.Verify(x => x.PrintServers(
                It.Is<List<Server>>(l => l == localServerList)
            ));
        }

        [TestMethod]
        public async Task RemoteServerListActionTest()
        {
            storageMock.Setup(m => m.GetConfiguration()).Returns(new Client() {
                Username = "api-username",
                Password = "api-password"
            });

            remoteApiMock.Setup(m => m.GetToken("api-username", "api-password")).Returns(
                Task.FromResult(new TokenResponse() {
                    token = "token-string"
                })
            );

            remoteApiMock.Setup(m => m.GetServers("Bearer token-string")).Returns(
                Task.FromResult(remoteServerList)
            );

            await CoreController
                .CreateWithProvider<ServerController>(serviceProvider)
                .ServerListAction(false);

            storageMock.Verify(x => x.SetSevers(
                It.Is<List<Server>>(list => list.Count > 0)
            ));

            // outputMock.Verify(x => x.PrintServers(
            //     It.Is<List<Server>>(l => l.Count > 0)
            // ));
        }
    }
}
