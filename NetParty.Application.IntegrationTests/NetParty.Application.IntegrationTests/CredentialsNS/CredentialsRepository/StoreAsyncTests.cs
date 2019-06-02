#region Using

using System.IO;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NetParty.Application.CredentialsNS;
using NetParty.Application.IntegrationTests.Common.Helpers;
using NUnit.Framework;

#endregion

namespace NetParty.Application.IntegrationTests.CredentialsNS.CredentialsRepository
    {
    [TestFixture]
    public class StoreAsyncTests
        {
        [Test]
        [AutoStorageData]
        public async Task CredentialsAreStored_StorageIsNotBeEmpty(
            [Frozen] ICredentialsStorageProvider storageProvider,
            Application.CredentialsNS.CredentialsRepository credentialsRepository)
            {
            // arrange
            var credentials = new Credentials("TestUsername", "TestPassword");

            // act
            await credentialsRepository.StoreAsync(credentials);

            // assert
            storageProvider.GetStorage().Length.Should().BeGreaterThan(0, "credentials should have been stored");
            }

        [Test]
        [AutoStorageData]
        public async Task CredentialsAreStored_CredentialsAreEncrypted(
            [Frozen] ICredentialsStorageProvider storageProvider,
            Application.CredentialsNS.CredentialsRepository credentialsRepository)
            {
            // arrange
            var credentials = new Credentials("TestUsername", "TestPassword");

            // act
            await credentialsRepository.StoreAsync(credentials);

            // assert
            string credentialStorageAsString;
            using (var streamReader = new StreamReader(storageProvider.GetStorage()))
                {
                credentialStorageAsString = await streamReader.ReadToEndAsync();
                }

            credentialStorageAsString.Should()
                .NotContainAny(new[] {credentials.Username, credentials.Password}, "Storing unencrypted credentials is unsafe");
            }
        }
    }
