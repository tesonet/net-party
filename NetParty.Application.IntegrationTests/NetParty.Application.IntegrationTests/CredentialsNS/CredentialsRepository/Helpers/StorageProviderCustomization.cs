#region Using

using AutoFixture;
using NetParty.Application.CredentialsNS;

#endregion

namespace NetParty.Application.IntegrationTests.CredentialsNS.CredentialsRepository.Helpers
    {
    public class StorageProviderCustomization : ICustomization
        {
        public void Customize(IFixture fixture)
            {
            fixture.Register(() => new MemoryStorageProvider() as IStorageProvider);
            }
        }
    }
