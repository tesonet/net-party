using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NetPartyCore.Controller;
using NetPartyCore.Datastore;
using NetPartyCore.Datastore.Model;
using NetPartyCore.Framework;
using NetPartyCore.Output;

namespace NetPartyTest.Controller
{
    [TestClass]
    public class ConfigControllerTest
    {
        private Mock<IStorage> storageMock;
        private Mock<IOutputFormatter> outputMock;
        private IServiceProvider serviceProvider;

        [TestInitialize]
        public void PrepareForTest()
        {
            storageMock = new Mock<IStorage>();
            outputMock = new Mock<IOutputFormatter>();

            serviceProvider = new ServiceCollection()
                .AddSingleton<IStorage>((_) => storageMock.Object)
                .AddSingleton<IOutputFormatter>((_) => outputMock.Object)
                .BuildServiceProvider();
        }

        [TestMethod]
        public void ConfigActionTest()
        {
            CoreController
                .CreateWithProvider<ConfigController>(serviceProvider)
                .ConfigAction("usrn", "pass");

            storageMock.Verify(x => x.SetConfiguration(
                It.Is<Client>(c => c.Username == "usrn" && c.Password == "pass")
            ));

            outputMock.Verify(x => x.PrintConfiguration(
                It.Is<Client>(c => c.Username == "usrn" && c.Password == "pass")
            ));
        }

    }
}
