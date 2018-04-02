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

namespace PartyCli.Tests.Commands
{
    [TestClass]
    public class ConfigCommandTest
    {
        List<Credentials> savedItems;
        Mock<IServiceProvider> servicesMock;
        Mock<ILogger> loggerMock;
        Mock<IRepository<Credentials>> repositoryMock;
        CommandLineApplication cliApp;
        ConfigCommand command;

        [TestInitialize]
        public void TestInitialize()
        {
            savedItems = null;

            loggerMock = new Mock<ILogger>(MockBehavior.Loose);
            loggerMock.Setup(l => l.ForContext<ConfigCommand>())
                .Returns(loggerMock.Object);

            repositoryMock = new Mock<IRepository<Credentials>>(MockBehavior.Strict);
            repositoryMock.Setup(r => r.Update(It.IsAny<IEnumerable<Credentials>>()))
                .Callback<IEnumerable<Credentials>>(items =>
                {
                    savedItems = items.ToList();
                });

            command = new ConfigCommand(loggerMock.Object, repositoryMock.Object);

            servicesMock = new Mock<IServiceProvider>(MockBehavior.Loose);
            servicesMock.Setup(s => s.GetService(typeof(ConfigCommand)))
                .Returns(command);

            cliApp = new CommandLineApplication();
            ConfigCommand.Configure(cliApp, servicesMock.Object);
        }

        [TestMethod]
        public void Configure_ShouldAdd_Description()
        {
            cliApp.Description.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Configure_ShouldAdd_Options()
        {
            cliApp.Options.Should().HaveCount(3);
        }

        [TestMethod]
        public void Configure_ShouldAdd_HelpOption()
        {
            cliApp.OptionHelp.Should().NotBeNull();
        }

        [TestMethod]
        public void Configure_ShouldAdd_UsernameOption()
        {
            cliApp.Options.Should()
                .ContainSingle(o => o.Template == "-u|--username <USERNAME>")
                .Which.OptionType.Should().Be(CommandOptionType.SingleValue);
        }

        [TestMethod]
        public void Configure_ShouldAdd_PasswordOption()
        {
            cliApp.Options.Should()
                .ContainSingle(o => o.Template == "-p|--password <PASSWORD>")
                .Which.OptionType.Should().Be(CommandOptionType.SingleValue);
        }

        [TestMethod]
        public void Execute_ShouldNotAdd_Commands()
        {
            cliApp.Commands.Should().HaveCount(0);
        }

        [TestMethod]
        public void Execute_ShouldCall_RepositoryUpdate_Once()
        {
            cliApp.Execute(new[] { "--username", "User's name", "--password", "My password" });

            repositoryMock.Verify(r => r.Update(It.IsAny<IEnumerable<Credentials>>()), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldSave_SingleItem()
        {
            cliApp.Execute(new[] { "--username", "User's name", "--password", "My password" });

            savedItems.Should().HaveCount(1);
        }

        [TestMethod]
        public void Execute_ShouldSave_Username()
        {
            cliApp.Execute(new[] { "--username", "User name", "--password", "Secret" });

            savedItems.First().Username.Should().Be("User name");
        }

        [TestMethod]
        public void Execute_ShouldSave_Password()
        {
            cliApp.Execute(new[] { "--username", "User name", "--password", "Secret" });

            savedItems.First().Password.Should().Be("Secret");
        }
    }
}
