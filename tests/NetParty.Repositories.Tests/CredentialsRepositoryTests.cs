using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Repositories.File;
using NUnit.Framework;

namespace NetParty.Repositories.Tests
{
    [TestFixture]
    public class CredentialsRepositoryTests
    {
        private const string FileName = "Secrets.sec";
        private readonly string _filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);

        [Test]
        public async Task CredentialsFileCreatedAndDeletedTest()
        {
            CredentialsRepository credentialsRepository = new CredentialsRepository();

            var credentials = new Credentials
            {
                UserName = "TestUser",
                Password = "TestPassword"
            };

            await credentialsRepository.SaveCredentialsAsync(credentials).ConfigureAwait(false);

            Assert.IsTrue(System.IO.File.Exists(_filePath));

            credentialsRepository.Dispose();

            Assert.IsFalse(System.IO.File.Exists(_filePath));
        }

        [Test]
        public async Task CredentialsIsRetunedTest()
        {
            CredentialsRepository credentialsRepository = new CredentialsRepository();

            var credentials = new Credentials
            {
                UserName = "TestUser",
                Password = "TestPassword"
            };

            await credentialsRepository.SaveCredentialsAsync(credentials).ConfigureAwait(false);

            Assert.IsTrue(System.IO.File.Exists(_filePath));

            var credentialsFromFile = await credentialsRepository.GetCredentialsAsync().ConfigureAwait(false);

            Assert.AreEqual(credentials.UserName, credentialsFromFile.UserName);
            Assert.AreEqual(credentials.Password, credentialsFromFile.Password);
        }

        [TearDown]
        public void TearDown()
        {
            System.IO.File.Delete(_filePath);
        }
    }
}
