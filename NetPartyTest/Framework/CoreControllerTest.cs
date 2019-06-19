using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using NetPartyCore.Framework;
using Moq;

namespace NetPartyTest.Framework
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class CoreControllerTest
    {
        
        [TestMethod]
        public void CreateWithProviderTest()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var service = new ServiceMock();

            serviceProviderMock
                .Setup(it => it.GetService<IServiceMock>())
                .Returns(service);

            var controller = CoreController
                .CreateWithProvider<ControllerMock>(serviceProviderMock.Object);

            Assert.IsTrue(controller.MethodMock());
        }
    }
}
