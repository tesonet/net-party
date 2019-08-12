using NUnit.Framework;
using Newtonsoft.Json;
using Unity;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using partycli.Repository;
using partycli.Config;
using partycli.Http;
using partycli.Helpers;

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
        public async Task IAuthenticationRepository_ValidateRetrieveToken_MakesHttpPostAsync()
        {
            //Arrange
            string serializedCredentials = JsonConvert.SerializeObject(
                new Credentials(AuthenticationRepository.Encrypt("foo"), AuthenticationRepository.Encrypt("bar")));
            mockRepositoryProvider.Setup(repo => repo.LoadAsync()).Returns(Task.FromResult(serializedCredentials));
            mockHttpService.Setup(http => http.PostJson(It.IsAny<string>())).Returns(Task.FromResult(new SuccessResult<string>("token") as IRequestResult<string>));

            //Act
            var response = await authRepository.RetrieveToken();

            //Assert
            mockHttpService.Verify(mock => mock.PostJson(It.IsAny<string>()), Times.Once());
            response.Success.Should().Be(true);
            response.Result.Should().Be("token");
        }
    }
}
