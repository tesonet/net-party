using System;
using System.Threading.Tasks;
using Moq;
using NetParty.Clients.Interfaces;
using NetParty.Contracts;
using NetParty.Contracts.Requests;
using NetParty.Repositories.Core;
using NetParty.Services.Interfaces;
using NUnit.Framework;

namespace NetParty.Handlers.Tests
{
    [TestFixture]
    public class ServerListHandlerTests
    {
        [Test]
        public void ConstructorTest()
        {
            Assert.Throws<ArgumentNullException>(() => new ServerListHandler(null, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new ServerListHandler(new Mock<ISecurityService>().Object, null, null, null));
            Assert.Throws<ArgumentNullException>(() => new ServerListHandler(new Mock<ISecurityService>().Object, new Mock<ITesonetClient>().Object, null, null));
            Assert.Throws<ArgumentNullException>(() => new ServerListHandler(new Mock<ISecurityService>().Object, new Mock<ITesonetClient>().Object, new Mock<IServersRepository>().Object, null));
        }

        [Test]
        public async Task GetLocalServersListTest()
        {
            ServerDto[] testList = {
                new ServerDto{Name = "TestName", Distance = 123}
            };

            var securityServiceMock = new Mock<ISecurityService>();
            var tesonetClientMock = new Mock<ITesonetClient>();
            var serversRepositoryMock = new Mock<IServersRepository>();
            serversRepositoryMock
                .Setup(x => x.GetServersAsync())
                .ReturnsAsync(testList);

            var displayServiceMock = new Mock<IDisplayService>();

            var handler = new ServerListHandler(
                securityServiceMock.Object,
                tesonetClientMock.Object,
                serversRepositoryMock.Object,
                displayServiceMock.Object
                );

            await handler.HandleBaseAsync(new ServerListRequest
            {
                Local = true
            });

            securityServiceMock.Verify(x => x.GetTokenAsync(), Times.Never);
            tesonetClientMock.Verify(x => x.GetServersAsync(It.IsAny<string>()), Times.Never);
            serversRepositoryMock.Verify(x => x.SaveServersAsync(It.IsAny<ServerDto[]>()), Times.Never);
            serversRepositoryMock.Verify(x => x.GetServersAsync(), Times.Once);
            displayServiceMock.Verify(x => x.DisplayTable(testList), Times.Once);
        }

        [Test]
        public async Task GetServersFromTesonetListTest()
        {
            ServerDto[] testList = {
                new ServerDto{Name = "TestName", Distance = 123}
            };

            var userToken = "token";

            var securityServiceMock = new Mock<ISecurityService>();
            securityServiceMock
                .Setup(x => x.GetTokenAsync())
                .ReturnsAsync(userToken);

            var tesonetClientMock = new Mock<ITesonetClient>();
            tesonetClientMock
                .Setup(x => x.GetServersAsync(userToken))
                .ReturnsAsync(testList);

            var serversRepositoryMock = new Mock<IServersRepository>();
            serversRepositoryMock
                .Setup(x => x.SaveServersAsync(testList))
                .ReturnsAsync(testList);

            var displayServiceMock = new Mock<IDisplayService>();

            var handler = new ServerListHandler(
                securityServiceMock.Object,
                tesonetClientMock.Object,
                serversRepositoryMock.Object,
                displayServiceMock.Object
            );

            await handler.HandleBaseAsync(new ServerListRequest());

            securityServiceMock.Verify(x => x.GetTokenAsync(), Times.Once);
            tesonetClientMock.Verify(x => x.GetServersAsync(userToken), Times.Once);
            serversRepositoryMock.Verify(x => x.SaveServersAsync(testList), Times.Once);
            serversRepositoryMock.Verify(x => x.GetServersAsync(), Times.Never);
            displayServiceMock.Verify(x => x.DisplayTable(testList), Times.Once);
        }
    }
}
