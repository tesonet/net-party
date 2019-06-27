using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using NetPartyCore.Framework;
using Moq;

namespace NetPartyTest.Framework
{
    [TestClass]
    public class CoreControllerTest
    {
        
        [TestMethod]
        public void CreateWithProviderTest()
        {
            var serviceMock = new Mock<IServiceMock>();

            // since extension C# extensions are just static and they are
            // b**** to test, a real ServiceProvider implementation is used
            // instead of a mock
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IServiceMock>((_) => serviceMock.Object)
                .BuildServiceProvider();

            var controller = CoreController
                .CreateWithProvider<ControllerMock>(serviceProvider);

            serviceMock.Setup(it => it.TestMthod())
                .Returns(true);

            Assert.IsTrue(controller.MethodMock());
        }
    }

    internal class ControllerMock : CoreController
    {
        public bool MethodMock()
        {
            return GetSerivce<IServiceMock>().TestMthod();
        }
    }

    internal interface IServiceMock
    {
        bool TestMthod();
    }
}
