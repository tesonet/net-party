using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extras.Moq;
using Moq;
using partycli.core.Contracts;
using partycli.core.DataAccess;
using partycli.core.Execution;
using partycli.core.Repositories.Model;
using partycli.core.Repositories.Storage;
using Xunit;


namespace partycli.Tests.UnitTests
{

    public class ExecutorTests
    {
        [Fact]
        public void Executor_SaveCredentialsIsWorking()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var mockStoreManager = mock.Mock<IStorageManager>();
                var mockApi = mock.Mock<IApiClient>();
                Credentials credentials = null;

                mockStoreManager
                    .Setup(x => x.SaveCredentials(It.IsAny<Credentials>()))
                    .Callback<Credentials>(c => credentials = c);
                  
                IExecutor executor = new Executor(mockApi.Object, mockStoreManager.Object);
                executor.SaveCredentials(new Credentials() { Username = "bob", Password = "123" });

                Assert.NotNull(credentials);
                Assert.Equal("bob", credentials.Username);
                Assert.Equal("123", credentials.Password);
            }
        }

        [Fact]
        public async Task Executor_FetchServersNotLoclIsWorking()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var mockStoreManager = mock.Mock<IStorageManager>();
                var mockApi = mock.Mock<IApiClient>();
                List<ServerContract> list = new List<ServerContract>() { new ServerContract() { Name = "test", Distance = 1 } };
                IEnumerable<Server> expected = new List<Server>() { new Server() { Name = "test", Distance = 1 } };

                mockStoreManager.Setup(x => x.GetCredentials())
                    .Returns(new Credentials() { Username = "t", Password = "b" });

                mockApi.Setup(x => x.GetToken(It.IsAny<CredentialsContract>()))
                    .Returns(Task.FromResult("token"));

                mockApi.Setup(x => x.GetServers(It.IsAny<string>()))
                    .Returns(Task.FromResult<IEnumerable<ServerContract>>(list));
                
                IExecutor executor = new Executor(mockApi.Object, mockStoreManager.Object);
                IEnumerable<Server> result = await executor.FetchServers(false);

                Assert.NotEmpty(result);
                Assert.Equal(expected.First().Name, result.First().Name);
                Assert.Equal(expected.First().Distance, result.First().Distance);
            }
        }

        [Fact]
        public async Task Executor_FetchServersLoclIsWorking()
        {
            using (var mock = AutoMock.GetLoose())
            {
                var mockStoreManager = mock.Mock<IStorageManager>();
                var mockApi = mock.Mock<IApiClient>();
                
                IEnumerable<Server> expected = new List<Server>() { new Server() { Name = "test", Distance = 1 } };

                mockStoreManager.Setup(x => x.GetServers())
                    .Returns(expected);
                
                IExecutor executor = new Executor(mockApi.Object, mockStoreManager.Object);
                IEnumerable<Server> result = await executor.FetchServers(true);

                Assert.NotEmpty(result);
                Assert.Equal(expected.First().Name, result.First().Name);
                Assert.Equal(expected.First().Distance, result.First().Distance);
            }
        }
        
    }
}
