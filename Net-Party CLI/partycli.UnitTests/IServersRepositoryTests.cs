using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using partycli.Http;
using partycli.Repository;
using Unity;
using partycli.Servers;
using FluentAssertions;
using partycli.Helpers;

namespace partycli.UnitTests.IServersRepositoryTests
{
    [TestFixture]
    class IServersRepositoryTests
    {
        private string server_list = null;
        private List<Server> server_list_deserialized = null;
        private Mock<IHttpService> mockHttpService = null;
        private Mock<IRepositoryProvider> mockRepositoryProvider = null;
        private IUnityContainer container = null;
        private IServersRepository serversRepository = null;

        [SetUp]
        public void TestSetup()
        {
            server_list = "[{\"name\": \"United States #3\",\"distance\": 627}, {\"name\": \"Japan #78\",\"distance\": 1107}]";
            server_list_deserialized = JsonConvert.DeserializeObject<List<Server>>(server_list);
            mockHttpService = new Mock<IHttpService>();
            mockHttpService.Setup(mock => mock.GetWithToken(It.IsAny<string>())).Returns(Task.FromResult(new SuccessResult<string>(server_list) as IRequestResult<string>));
            mockRepositoryProvider = new Mock<IRepositoryProvider>();

            container = new UnityContainer();
            container.RegisterInstance<IHttpService>(mockHttpService.Object);
            container.RegisterInstance<IRepositoryProvider>(mockRepositoryProvider.Object);

            serversRepository = new ServersRepository(httpService: container.Resolve<IHttpService>(), serversRepositoryProvider: container.Resolve<IRepositoryProvider>());
        }

        [Test]
        public async Task IServersRepositoryTests_RetrieveServersListAsync_()
        {
            //Arrange
            mockHttpService.Setup(mock => mock.GetWithToken(It.IsAny<string>())).Returns(Task.FromResult(new SuccessResult<string>(server_list) as IRequestResult<string>));

            //Act
            var result = await serversRepository.RetrieveServersListAsync("token");
            
            //Assert
            mockHttpService.Verify(mock => mock.GetWithToken(It.IsAny<string>()), Times.Once());
            mockRepositoryProvider.Verify(mock => mock.Reset(), Times.Once());
            mockRepositoryProvider.Verify(mock => mock.SaveAsync(It.IsAny<string>()), Times.Once());
            result.Success.Should().Be(true);
            result.Result.Should().HaveCount(2).And.Should().Equals(server_list_deserialized);
        }

        [Test]
        public async Task IServersRepositoryTests_RetrieveServersListLocalAsync_()
        {
            //Arrange
            mockRepositoryProvider.Setup(mock => mock.LoadAsync()).Returns(Task.FromResult(server_list));

            //Act
            var result = await serversRepository.RetrieveServersListLocalAsync();
            
            //Assert
            mockHttpService.Verify(mock => mock.GetWithToken(It.IsAny<string>()), Times.Never());
            mockRepositoryProvider.Verify(mock => mock.LoadAsync(), Times.Once());
            result.Should().HaveCount(2).And.Should().Equals(server_list_deserialized);
        }
    }
}
