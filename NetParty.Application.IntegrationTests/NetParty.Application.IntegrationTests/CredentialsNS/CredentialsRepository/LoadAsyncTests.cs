#region Using

using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NetParty.Application.CredentialsNS;
using NetParty.Application.IntegrationTests.CredentialsNS.CredentialsRepository.Helpers;
using NUnit.Framework;

#endregion

namespace NetParty.Application.IntegrationTests.CredentialsNS.CredentialsRepository
    {
    [TestFixture]
    public class LoadAsyncTests
        {
        [Test]
        [AutoStorageData]
        public async Task CredentialsAreStored_CredentialsAreLoaded(
            [Frozen] IStorageProvider storageProvider,
            Application.CredentialsNS.CredentialsRepository credentialsRepository)
            {
            // arrange
            var storedCredentials = new Credentials {Username = "TestUsername", Password = "TestPassword"};
            await credentialsRepository.StoreAsync(storedCredentials);

            // act
            var loadedCredentials = await credentialsRepository.LoadAsync();

            // assert
            loadedCredentials.Should().Be(storedCredentials);
            }

        [Test]
        [AutoStorageData]
        public async Task NoCredentialsAreStored_NullReturned(
            [Frozen] IStorageProvider storageProvider,
            Application.CredentialsNS.CredentialsRepository credentialsRepository)
            {
            // arrange

            // act
            var loadedCredentials = await credentialsRepository.LoadAsync();

            // assert
            loadedCredentials.Should().BeNull();
            }
        }
    }
