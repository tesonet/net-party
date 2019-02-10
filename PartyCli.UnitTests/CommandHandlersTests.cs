using Castle.Core.Logging;
using Moq;
using NUnit.Framework;
using PartyCli.Commands;
using PartyCli.Entities;
using PartyCli.Interfaces;
using PartyCli.Services.Interfaces;
using System.Collections.Generic;

namespace PartyCli.UnitTests
{
  [TestFixture]
  public class CommandHandlersTests
  {   
    private Mock<IServerManagementService> _serviceMock;
    private Mock<IConsoleWriter> _consoleWriterMock;

    private ConfigCommandHandler _configHandler;
    private ServerListCommandHandler _serverListHandler;

    [SetUp]
    public void Setup()
    {
      _consoleWriterMock = new Mock<IConsoleWriter>();
      
      _serviceMock = new Mock<IServerManagementService>();
      _serviceMock.Setup(x => x.FetchServersAsync(It.IsAny<bool>()))
        .ReturnsAsync(new List<Server>() {
           new Server() { Name = "Test #22", Distance = 1458 },
          new Server() { Name = "Test #87", Distance = 491 },
        });

      _configHandler = new ConfigCommandHandler(_serviceMock.Object, _consoleWriterMock.Object);
      _serverListHandler = new ServerListCommandHandler(_serviceMock.Object, _consoleWriterMock.Object);
    }

    [Test]
    public void ConfigCommandHandler_Should_Call_SaveCredentials()
    {
      var exitcode = _configHandler.HandleAndReturnExitCode(new ConfigOptions()
      {
        Username = "test-user",
        Password = "test-password",
      });

      Assert.IsTrue(exitcode == 0);

      _serviceMock.Verify(x => x.SaveCredentials(It.IsAny<string>(), It.IsAny<string>()), Times.Once);      
    }

    [TestCase(true)]
    [TestCase(false)]
    public void ServerListCommandHandler_Should_Call_FetchServersAsync(bool local)
    {
      var exitcode = _serverListHandler.HandleAndReturnExitCode(new ServerListOptions()
      {
        Clear = false,
        Local = local,
      });

      Assert.IsTrue(exitcode == 0);

      _serviceMock.Verify(x => x.ClearServers(), Times.Never);
      _serviceMock.Verify(x => x.FetchServersAsync(It.IsAny<bool>()), Times.Once);      
    }
    
    [Test]
    public void ServerListCommandHandler_Should_Call_ClearServers()
    {
      var exitcode = _serverListHandler.HandleAndReturnExitCode(new ServerListOptions()
      {
        Clear = true,
        Local = true,
      });

      Assert.IsTrue(exitcode == 0);

      _serviceMock.Verify(x => x.ClearServers(), Times.Once);
      _serviceMock.Verify(x => x.FetchServersAsync(It.IsAny<bool>()), Times.Never);
    }
  }
}
