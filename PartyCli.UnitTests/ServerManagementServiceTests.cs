using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Castle.Core.Logging;
using PartyCli.Entities;
using PartyCli.Repositories;
using PartyCli.WebApiClient.Interfaces;
using PartyCli.Services;
using PartyCli.WebApiClient.DataContracts;

namespace PartyCli.UnitTests
{
  [TestFixture]
  public class ServerManagementServiceTests
  {
    private Mock<ILogger> _loggerMock;
    private Mock<IRepository<Credentials>> _credentialsRepositoryMock;
    private Mock<IRepository<Server>> _serverRepositoryMock;
    private Mock<IWebApiClient> _webApiClientMock;

    private ICollection<Credentials> _credentials;
    private ICollection<Server> _servers;
    private ServerManagementService _service;

    [SetUp]
    public void Setup()
    {
      _credentials = new List<Credentials>()
      {
        new Credentials { UserName = "test-username", Password = "test-password" }
      };

      _servers = new List<Server>
      {
        new Server() { Name = "Test #22", Distance = 1458 },
        new Server() { Name = "Test #87", Distance = 491 },
        new Server() { Name = "Test #53", Distance = 229 },
      };

      _loggerMock = new Mock<ILogger>();

      _credentialsRepositoryMock = new Mock<IRepository<Credentials>>();
      _credentialsRepositoryMock.Setup(x => x.FindAll())
        .Returns(_credentials);
      _credentialsRepositoryMock.Setup(x => x.InsertBulk(It.IsAny<IEnumerable<Credentials>>()))
        .Callback<IEnumerable<Credentials>>(x =>
        {
          _credentials = x.ToList();
        });
      _credentialsRepositoryMock.Setup(x => x.Truncate())
       .Callback(() =>
       {
         _credentials.Clear();
       });

      _serverRepositoryMock = new Mock<IRepository<Server>>();
      _serverRepositoryMock.Setup(x => x.FindAll())
        .Returns(_servers);
      _serverRepositoryMock.Setup(x => x.InsertBulk(It.IsAny<IEnumerable<Server>>()))
        .Callback<IEnumerable<Server>>(x =>
        {
          _servers = x.ToList();
        });
      _serverRepositoryMock.Setup(x => x.Truncate())
       .Callback(() =>
       {
         _servers.Clear();
       });

      _webApiClientMock = new Mock<IWebApiClient>();
      _webApiClientMock.Setup(x => x.GetTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
        .ReturnsAsync(new TokenDataContract() { Token = "test-token" });

      _webApiClientMock.Setup(x => x.GetServersAsync(It.IsAny<string>()))
      .ReturnsAsync(new List<ServerDataContract> {
        new ServerDataContract() { Name = "Test #22", Distance = 1458 },
        new ServerDataContract() { Name = "Test #87", Distance = 491 },
        new ServerDataContract() { Name = "Test #53", Distance = 229 },
        new ServerDataContract() { Name = "Test #4", Distance = 275 },
        new ServerDataContract() { Name = "Test #80", Distance = 1738 },
      });

      _service = new ServerManagementService(
        _webApiClientMock.Object,
        _credentialsRepositoryMock.Object,
        _serverRepositoryMock.Object,
        _loggerMock.Object);
    }

    [Test]
    public async Task Service_Should_Fetch_Servers_From_Local_Storage()
    {
      var servers = await _service.FetchServersAsync(true);

      Assert.IsTrue(servers.Count == 3);
      _serverRepositoryMock.Verify(x => x.FindAll(), Times.Once);
      _webApiClientMock.Verify(x => x.GetServersAsync(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public async Task Service_Should_Fetch_Servers_From_WebApi()
    {
      var servers = await _service.FetchServersAsync(false);

      Assert.IsTrue(servers.Count == 5);
      _credentialsRepositoryMock.Verify(x => x.FindAll(), Times.Once);
      _serverRepositoryMock.Verify(x => x.FindAll(), Times.Never);
      _webApiClientMock.Verify(x => x.GetTokenAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
      _webApiClientMock.Verify(x => x.GetServersAsync(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void Service_Should_Clear_Servers_From_Local_Storage()
    {
      _service.ClearServers();

      Assert.IsFalse(_servers.Any());
      _serverRepositoryMock.Verify(x => x.Truncate(), Times.Once);
    }

    [Test]
    public void Service_Should_Save_Servers_To_Local_Storage()
    {
      var servers = new List<Server>()
      {
        new Server() { Name = "Test-New #22", Distance = 1458 },
        new Server() { Name = "Test-New #87", Distance = 491 },
      };

      _service.SaveServers(servers);

      Assert.IsTrue(servers.Count == _servers.Count);

      _serverRepositoryMock.Verify(x => x.InsertBulk(It.IsAny<IEnumerable<Server>>()), Times.Once);
      _serverRepositoryMock.Verify(x => x.Truncate(), Times.Once);
    }

    [TestCase("test-user-new", "test-password-new")]
    public void Service_Should_Save_Credentials_To_Local_Storage(string username, string password)
    {
      _service.SaveCredentials(username, password);

      var credentials = _credentials.First();

      Assert.AreEqual(username, credentials.UserName);
      Assert.AreEqual(password, credentials.Password);

      _credentialsRepositoryMock.Verify(x => x.Truncate(), Times.Once);
      _credentialsRepositoryMock.Verify(x => x.InsertBulk(It.IsAny<IEnumerable<Credentials>>()), Times.Once);
    }
  }
}
