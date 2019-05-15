using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Moq;
using NetParty.Contracts;
using NetParty.Contracts.Exceptions;
using NetParty.Interfaces.Clients;
using NetParty.Interfaces.Repositories;
using NetParty.Interfaces.Services;
using NetParty.Services;
using NUnit.Framework;

namespace NetParty.UnitTests
{
    [TestFixture]
    public class ServerListServiceTests
    {
        private ServerListService _service;
        private Mock<ICredentialsRepository> _credentialsRepositoryMock;
        private Mock<IAuthorizationClient> _authorizationClientMock;
        private Mock<IServersClient> _serversClientMock;
        private Mock<IServersRepository> _serversRepositoryMock;
        private Mock<IOutputService> _outputServiceMock;
        private List<Server> _mockedServersList;
        private Fixture _fixture;

        [SetUp]
        public void Init()
        {
            _credentialsRepositoryMock = new Mock<ICredentialsRepository>();
            _authorizationClientMock = new Mock<IAuthorizationClient>();
            _serversClientMock = new Mock<IServersClient>();
            _serversRepositoryMock = new Mock<IServersRepository>();
            _outputServiceMock = new Mock<IOutputService>();
            _fixture = new Fixture();
            _mockedServersList = _fixture.CreateMany<Server>().ToList();

            _service = new ServerListService(_credentialsRepositoryMock.Object, _authorizationClientMock.Object, _serversClientMock.Object,
                _serversRepositoryMock.Object, _outputServiceMock.Object);
        }

        [Test]
        public async Task When_PrintingLocalServerList_Expect_OutputServiceWithCorrectServersIsCalled()
        {
            _serversRepositoryMock.Setup(x => x.GetServers()).ReturnsAsync(_mockedServersList);

            await _service.PrintServerList(true);

            _outputServiceMock.Verify(x => x.OutputServers(It.Is<List<Server>>(l => l.Equals(_mockedServersList) )), Times.Once);
        }

        [Test]
        public void Given_NoCredentialsLoaded_When_PrintingServerList_Expect_ValidationException()
        {
            _credentialsRepositoryMock.Setup(x => x.LoadCredentials()).ReturnsAsync((Credentials)null);

            Assert.ThrowsAsync<BaseValidationException>(async () => await _service.PrintServerList(false));
        }

        [Test]
        public void Given_NoTokenFetched_When_PrintingServerList_Expect_ValidationException()
        {
            _credentialsRepositoryMock.Setup(x => x.LoadCredentials()).ReturnsAsync(_fixture.Create<Credentials>());
            var mockedServersList = _fixture.CreateMany<Server>().ToList();
            _serversClientMock.Setup(x => x.FetchServers(It.IsAny<string>())).ReturnsAsync(mockedServersList);

            Assert.ThrowsAsync<BaseValidationException>(async () => await _service.PrintServerList(false));
        }

        [Test]
        public async Task When_PrintingServerList_Expect_ServersToBeSaved()
        {
            _credentialsRepositoryMock.Setup(x => x.LoadCredentials()).ReturnsAsync(_fixture.Create<Credentials>());
            var mockedServersList = _fixture.CreateMany<Server>().ToList();
            _serversClientMock.Setup(x => x.FetchServers(It.IsAny<string>())).ReturnsAsync(mockedServersList);

            await _service.PrintServerList(false);

            _serversRepositoryMock.Verify(x => x.SaveServers(mockedServersList), Times.Once);
        }

        [Test]
        public async Task When_PrintingServerList_Expect_ServersToBePrinted()
        {
            _credentialsRepositoryMock.Setup(x => x.LoadCredentials()).ReturnsAsync(_fixture.Create<Credentials>());
            _serversClientMock.Setup(x => x.FetchServers(It.IsAny<string>())).ReturnsAsync(_mockedServersList);

            await _service.PrintServerList(false);

            _outputServiceMock.Verify(x => x.OutputServers(It.Is<List<Server>>(l => l.Equals(_mockedServersList))),
                Times.Once);
        }
    }
}
