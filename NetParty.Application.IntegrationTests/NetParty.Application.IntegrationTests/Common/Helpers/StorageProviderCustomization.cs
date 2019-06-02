#region Using

using AutoFixture;
using NetParty.Application.CredentialsNS;
using NetParty.Application.Servers;

#endregion

namespace NetParty.Application.IntegrationTests.Common.Helpers
    {
    public class StorageProviderCustomization : ICustomization
        {
        public void Customize(IFixture fixture)
            {
            fixture.Register(() => new MemoryStorageProvider() as ICredentialsStorageProvider);
            fixture.Register(() => new MemoryStorageProvider() as IServerStorageProvider);
            }
        }
    }
