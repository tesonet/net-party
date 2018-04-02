using FluentAssertions;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PartyCli.Commands;
using System;

namespace PartyCli.Tests.Commands
{
    [TestClass]
    public class AppTest
    {
        Mock<IServiceProvider> servicesMock;

        CommandLineApplication cliApp;

        [TestInitialize]
        public void TestInitialize()
        {
            servicesMock = new Mock<IServiceProvider>(MockBehavior.Loose);

            cliApp = new CommandLineApplication();
            App.Configure(cliApp, servicesMock.Object);
        }

        [TestMethod]
        public void Configure_ShouldAdd_Name()
        {
            cliApp.Name.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void Configure_ShouldAdd_Options()
        {
            cliApp.Options.Should().HaveCount(1);
        }

        [TestMethod]
        public void Configure_ShouldAdd_HelpOption()
        {
            cliApp.OptionHelp.Should().NotBeNull();
        }

        [TestMethod]
        public void Execute_ShouldAdd_Commands()
        {
            cliApp.Commands.Should().HaveCount(2);
        }

        [TestMethod]
        public void Execute_ShouldNotAdd_ConfigCommand()
        {
            cliApp.Commands.Should().ContainSingle(c => c.Name == "config");
        }

        [TestMethod]
        public void Execute_ShouldNotAdd_ServerListCommand()
        {
            cliApp.Commands.Should().ContainSingle(c => c.Name == "server_list");
        }
    }
}
