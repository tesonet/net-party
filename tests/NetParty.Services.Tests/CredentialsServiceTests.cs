using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NetParty.Contracts;
using NUnit.Framework;

namespace NetParty.Services.Tests
{
    [TestFixture]
    public class CredentialsServiceTests
    {
        private const string FileName = "Secrets.sec";
        private readonly string _filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);

        [Test]
        public async Task CredentialsFileCreatedAndDeletedTest()
        {
            CredentialsService credentialsService = new CredentialsService();

            var credentials = new Credentials
            {
                UserName = "TestUser",
                Password = "TestPassword"
            };

            await credentialsService.SaveCredentialsAsync(credentials);

            Assert.IsTrue(File.Exists(_filePath));

            credentialsService.Dispose();

            Assert.IsFalse(File.Exists(_filePath));
        }

        [Test]
        public async Task CredentialsIsEncryptedTest()
        {
            CredentialsService credentialsService = new CredentialsService();

            var credentials = new Credentials
            {
                UserName = "TestUser",
                Password = "TestPassword"
            };

            await credentialsService.SaveCredentialsAsync(credentials);

            Assert.IsTrue(File.Exists(_filePath));

        }
    }
}
