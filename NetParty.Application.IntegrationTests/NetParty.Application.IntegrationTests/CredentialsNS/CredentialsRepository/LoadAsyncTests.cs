#region Using

using System.Threading.Tasks;
using FluentAssertions;
using NetParty.Application.CredentialsNS;
using NetParty.Application.IntegrationTests.Common.Helpers;
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
            Application.CredentialsNS.CredentialsRepository credentialsRepository)
            {
            // arrange
            var storedCredentials = new Credentials("TestUsername", "TestPassword");
            await credentialsRepository.StoreAsync(storedCredentials);

            // act
            var loadedCredentials = await credentialsRepository.LoadAsync();

            // assert
            loadedCredentials.Should().Be(storedCredentials);
            }

        [Test]
        [AutoStorageData]
        public async Task NoCredentialsAreStored_NullReturned(
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
