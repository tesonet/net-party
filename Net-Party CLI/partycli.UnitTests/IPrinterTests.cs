using System.Collections.Generic;
using NUnit.Framework;
using log4net;
using Unity;
using Moq;
using partycli.Helpers;

namespace partycli.UnitTests.IPrinterTests
{
    [TestFixture]
    class IPrinterTests
    {
        IUnityContainer container = null;
        Mock<ILog> mockLog = null;
        IPrinter printer = null;

        [SetUp]
        public void TestSetup()
        {
            container = new UnityContainer();
            mockLog = new Mock<ILog>();
            container.RegisterInstance<ILog>(mockLog.Object);
            printer = new Printer(container.Resolve<ILog>());
        }

        [Test]
        public void Printer_PrintServerList_Successful()
        {
            //Arrange
            //Act
            Assert.That(() => printer.ServersList(new List<Server> { new Server("a", 1), new Server("b", 2) }), Throws.InvalidOperationException);
            ;
            //Assert
            mockLog.Verify(log => log.Info(It.IsAny<string>()), Times.Exactly(2+1));
        }

        [Test]
        public void Printer_Error_Successful()
        {
            //Arrange
            //Act
            Assert.That(() => printer.Error("error message"), Throws.InvalidOperationException);
            ;
            //Assert
            mockLog.Verify(log => log.Error(It.IsAny<string>()), Times.Once);
            mockLog.Verify(log => log.Info(It.IsAny<string>()), Times.Exactly(0+1));
        }

        [Test]
        public void Printer_Info_Successful()
        {
            //Arrange
            //Act
            Assert.That(() => printer.Info("message"), Throws.InvalidOperationException);
            //Assert
            mockLog.Verify(log => log.Info(It.IsAny<string>()), Times.Exactly(1+1));
        }
    }
}
