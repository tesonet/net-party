using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using NetParty.Application;
using NUnit.Framework;

namespace NetParty.Services.Tests
{
    [TestFixture]
    public class CredentialsServiceTests
    {
        private const string FileName = "Secrets.sec";
        private readonly string SecretFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);

        [Test]
        public async Task CredentialsFileCreatedAndDeletedTest()
        {
            CredentialsService credentialsService = new CredentialsService();

            await credentialsService.SaveCredentialsAsync("TestUser", "TestPassword");

            Assert.IsTrue(File.Exists(SecretFilePath));

            credentialsService.Dispose();

            Assert.IsFalse(File.Exists(SecretFilePath));
        }

        [Test]
        public async Task CredentialsIsEncryptedTest()
        {
            CredentialsService credentialsService = new CredentialsService();

            await credentialsService.SaveCredentialsAsync("TestUser", "TestPassword");

            Assert.IsTrue(File.Exists(SecretFilePath));

        }
    }
}
