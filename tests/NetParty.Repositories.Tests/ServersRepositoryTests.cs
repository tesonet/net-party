using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Repositories.File;
using NUnit.Framework;

namespace NetParty.Repositories.Tests
{
    [TestFixture]
    public class ServersRepositoryTests
    {
        private const string FileName = "Servers.data";
        private readonly string _filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);

        [Test]
        public async Task CredentialsFileCreatedAndDeletedTest()
        {
            var serversRepository = new ServersRepository();

            ServerDto[] data =
            {
                new ServerDto
                {
                    Name = "test",
                    Distance = 1234
                }
            };
            await serversRepository.SaveServersAsync(data).ConfigureAwait(false);

            Assert.IsTrue(System.IO.File.Exists(_filePath));
        }

        [Test]
        public async Task CredentialsIsRetunedTest()
        {
            var serversRepository = new ServersRepository();

            ServerDto[] data =
            {
                new ServerDto
                {
                    Name = "test",
                    Distance = 1234
                }
            };
            await serversRepository.SaveServersAsync(data).ConfigureAwait(false);

            Assert.IsTrue(System.IO.File.Exists(_filePath));

            var dataFromFile = await serversRepository.GetServersAsync().ConfigureAwait(false);

            Assert.AreEqual(data[0].Name, dataFromFile[0].Name);
            Assert.AreEqual(data[0].Distance, dataFromFile[0].Distance);
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.File.Delete(_filePath);
        }
    }
}
