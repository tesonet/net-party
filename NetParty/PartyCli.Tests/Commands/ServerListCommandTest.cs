using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PartyCli.Commands;
using PartyCli.Interfaces;
using PartyCli.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PartyCli.Tests.Commands
{
    [TestClass]
    public class ServerListCommandTest
    {
        Credentials credentialsToReturn;
        List<Server> serversToReturn;
        Credentials serverApiAuthorizeCredentials;
        List<Server> savedItems;
        List<Server> displyedItems;

        Mock<IServiceProvider> servicesMock;
        Mock<ILogger> loggerMock;
        Mock<IRepository<Credentials>> credentialsRepositoryMock;
        Mock<IRepository<Server>> repositoryMock;
        Mock<IServerApi> serverApiMock;
        Mock<IServerPresenter> serverPresenterMock;
        CommandLineApplication cliApp;
        ServerListCommand command;

        [TestInitialize]
        public void TestInitialize()
        {
            credentialsToReturn = new Credentials
            {
                Username = "User's name",
                Password = "Top secret"
            };
            serversToReturn = new List<Server>()
            {
                new Server { Name = "First server" },
                new Server { Name = "Second server" },
                new Server { Name = "Third server" }
            };
            serverApiAuthorizeCredentials = null;
            savedItems = null;
            displyedItems = null;

            loggerMock = new Mock<ILogger>(MockBehavior.Loose);
            loggerMock.Setup(l => l.ForContext<ServerListCommand>())
                .Returns(loggerMock.Object);

            credentialsRepositoryMock = new Mock<IRepository<Credentials>>(MockBehavior.Strict);
            credentialsRepositoryMock.Setup(r => r.GetAll())
                .Returns(new[] { credentialsToReturn });

            repositoryMock = new Mock<IRepository<Server>>(MockBehavior.Strict);
            repositoryMock.Setup(r => r.GetAll())
                .Returns(new List<Server>(serversToReturn));
            repositoryMock.Setup(r => r.Update(It.IsAny<IEnumerable<Server>>()))
                .Callback<IEnumerable<Server>>(items =>
                {
                    savedItems = items.ToList();
                });

            serverApiMock = new Mock<IServerApi>(MockBehavior.Strict);
            serverApiMock.Setup(a => a.AuthorizeAsync(It.IsAny<Credentials>()))
                .Callback<Credentials>(credentials =>
                {
                    serverApiAuthorizeCredentials = credentials;
                })
                .Returns(Task.CompletedTask);
            serverApiMock.Setup(a => a.GetServersAsync())
                .ReturnsAsync(new List<Server>(serversToReturn).AsEnumerable());

            serverPresenterMock = new Mock<IServerPresenter>(MockBehavior.Strict);
            serverPresenterMock.Setup(p => p.Display(It.IsAny<IEnumerable<Server>>()))
                .Callback<IEnumerable<Server>>(items =>
                {
                    displyedItems = items.ToList();
                });

            command = new ServerListCommand(loggerMock.Object, serverApiMock.Object, repositoryMock.Object, credentialsRepositoryMock.Object, serverPresenterMock.Object);

            servicesMock = new Mock<IServiceProvider>(MockBehavior.Loose);
            servicesMock.Setup(s => s.GetService(typeof(ServerListCommand)))
                .Returns(command);

            cliApp = new CommandLineApplication();
            ServerListCommand.Configure(cliApp, servicesMock.Object);
        }

        [TestMethod]
        public void Configure_ShouldAdd_Description()
        {
            cliApp.Description.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Configure_ShouldAdd_Options()
        {
            cliApp.Options.Should().HaveCount(2);
        }

        [TestMethod]
        public void Configure_ShouldAdd_HelpOption()
        {
            cliApp.OptionHelp.Should().NotBeNull();
        }

        [TestMethod]
        public void Configure_ShouldAdd_LocalOption()
        {
            cliApp.Options.Should()
                .ContainSingle(o => o.Template == "-l|--local")
                .Which.OptionType.Should().Be(CommandOptionType.NoValue);
        }

        [TestMethod]
        public void Execute_ShouldNotAdd_Commands()
        {
            cliApp.Commands.Should().HaveCount(0);
        }

        [TestMethod]
        public void Execute_ShouldCall_RepositoryGetAll_WhenLocal()
        {
            cliApp.Execute(new[] { "--local" });

            repositoryMock.Verify(r => r.GetAll(), Times.Once);
            repositoryMock.Verify(r => r.Update(It.IsAny<IEnumerable<Server>>()), Times.Never);
        }

        [TestMethod]
        public void Execute_ShouldNotCall_CredentialsRepository_WhenLocal()
        {
            cliApp.Execute(new[] { "--local" });

            credentialsRepositoryMock.Verify(r => r.GetAll(), Times.Never);
        }

        [TestMethod]
        public void Execute_ShouldNotCall_SerevrApi_WhenLocal()
        {
            cliApp.Execute(new[] { "--local" });

            serverApiMock.Verify(a => a.AuthorizeAsync(It.IsAny<Credentials>()), Times.Never);
            serverApiMock.Verify(a => a.GetServersAsync(), Times.Never);
        }

        [TestMethod]
        public void Execute_ShouldCall_ServersPresenter_WhenLocal()
        {
            cliApp.Execute(new[] { "--local" });

            serverPresenterMock.Verify(a => a.Display(It.IsAny<IEnumerable<Server>>()), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldDisplay_Servers_WhenLocal()
        {
            cliApp.Execute(new[] { "--local" });

            displyedItems.Should().HaveCount(3)
                .And.BeEquivalentTo(serversToReturn);
        }

        [TestMethod]
        public void Execute_ShouldGet_Credentials()
        {
            cliApp.Execute();

            credentialsRepositoryMock.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldCall_ServerApi_Authorize()
        {
            cliApp.Execute();

            serverApiMock.Verify(a => a.AuthorizeAsync(It.IsAny<Credentials>()), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldCall_ServerApi_Authorize_WithCredentials()
        {
            cliApp.Execute();

            serverApiAuthorizeCredentials.Should().BeEquivalentTo(credentialsToReturn);
        }

        [TestMethod]
        public void Execute_ShouldCall_ServerApi_GetServers()
        {
            cliApp.Execute();

            serverApiMock.Verify(a => a.GetServersAsync(), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldCall_ServerRepository_Update()
        {
            cliApp.Execute();

            repositoryMock.Verify(a => a.Update(It.IsAny<IEnumerable<Server>>()), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldCall_ServerRepository_Update_WithServers()
        {
            cliApp.Execute();

            savedItems.Should().NotBeNull()
                .And.BeEquivalentTo(serversToReturn);
        }

        [TestMethod]
        public void Execute_ShouldDisplay_Servers()
        {
            cliApp.Execute();

            displyedItems.Should().BeEquivalentTo(serversToReturn);
        }
    }
}
