#region Using

using System.IO;
using NetParty.Application.CredentialsNS;
using NetParty.Application.Servers;

#endregion

namespace NetParty.Application.IntegrationTests.Common.Helpers
    {
    public class MemoryStorageProvider : ICredentialsStorageProvider, IServerStorageProvider
        {
        private readonly DeferredDisposalMemoryStream m_storage = new DeferredDisposalMemoryStream();

        public Stream GetStorage()
            {
            m_storage.Seek(0, SeekOrigin.Begin);
            return m_storage;
            }
        }
    }
