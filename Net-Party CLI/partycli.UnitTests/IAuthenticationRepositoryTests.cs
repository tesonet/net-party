using NUnit.Framework;
using Newtonsoft.Json;
using Unity;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using partycli.Repository;
using partycli.Config;
using partycli.Http;

namespace partycli.UnitTests.IAuthenticationRepositoryTests
{
    [TestFixture]
    class IAuthenticationRepositoryTests
    {
        IUnityContainer container = null;
        Mock<IHttpService> mockHttpService = null;
        Mock<IRepositoryProvider> mockRepositoryProvider = null;
        AuthenticationRepository authRepository = null;

        [SetUp]
        public void TestSetup()
        {
            //Arrange  
            container = new UnityContainer();
            mockHttpService = new Mock<IHttpService>();            
            mockRepositoryProvider = new Mock<IRepositoryProvider>();
            container.RegisterInstance<IHttpService>(mockHttpService.Object);
            container.RegisterInstance<IRepositoryProvider>(mockRepositoryProvider.Object);
            authRepository = new AuthenticationRepository(httpService: container.Resolve<IHttpService>(), repositoryProvider: container.Resolve<IRepositoryProvider>());
        }

        [Test]
        public void IAuthenticationRepository_ValidateSave_ResetsFile()
        {
            //Arrange            

            //Act
            authRepository.SaveCredentialsAsync("username", "password");

            //Assert
            mockRepositoryProvider.Verify(mock => mock.Reset(), Times.Once());
        }

        [Test]
        public void IAuthenticationRepository_ValidateSave_SavesFile()
        {
            //Arrange
            
            //Act
            authRepository.SaveCredentialsAsync("username", "password");

            //Assert
            mockRepositoryProvider.Verify(mock => mock.SaveAsync(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void IAuthenticationRepository_ValidateSave_SavesFileWithEncryptedCredentials()
        {
            //Arrange
            string serializedCredentials = JsonConvert.SerializeObject(
                new Credentials(AuthenticationRepository.Encrypt("username"), AuthenticationRepository.Encrypt("password")));

            //Act
            authRepository.SaveCredentialsAsync("username", "password");

            //Assert
            mockRepositoryProvider.Verify(mock => mock.SaveAsync(serializedCredentials), Times.Once());
        }

        [Test]
        public void IAuthenticationRepository_ValidateLoad_LoadsFromProvider()
        {
            //Arrange
            string serializedCredentials = JsonConvert.SerializeObject(
                new Credentials(AuthenticationRepository.Encrypt("foo"), AuthenticationRepository.Encrypt("bar")));
            mockRepositoryProvider.Setup(repo => repo.LoadAsync()).Returns(Task.FromResult(serializedCredentials));

            //Act
            authRepository.LoadCredentialsAsync().Wait();

            //Assert
            mockRepositoryProvider.Verify(mock => mock.LoadAsync(), Times.Once());
        }

        [Test]
        public async Task IAuthenticationRepository_ValidateLoad_ReturnsCredentials()
        {
            //Arrange
            string serializedCredentials = JsonConvert.SerializeObject(
                new Credentials(AuthenticationRepository.Encrypt("username"), AuthenticationRepository.Encrypt("password")));
            mockRepositoryProvider.Setup(repo => repo.LoadAsync()).Returns(Task.FromResult(serializedCredentials));

            //Act
            Credentials credentialsResult = await authRepository.LoadCredentialsAsync();

            //Assert
            credentialsResult.Should().NotBeNull();
            credentialsResult.Should().Equals(new Credentials("username", "password"));
        }

        [Test]
        public void IAuthenticationRepository_ValidateRetrieveToken_MakesHttpPost()
        {
            //Arrange
            string serializedCredentials = JsonConvert.SerializeObject(
                new Credentials(AuthenticationRepository.Encrypt("foo"), AuthenticationRepository.Encrypt("bar")));
            mockRepositoryProvider.Setup(repo => repo.LoadAsync()).Returns(Task.FromResult(serializedCredentials));

            //Act
            Task<string> token = authRepository.RetrieveToken();

            //Assert
            mockHttpService.Verify(mock => mock.PostJson(It.IsAny<string>()), Times.Once());
        }
    }
}
