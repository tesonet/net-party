#region Using

using System.IO;
using NetParty.Application.CredentialsNS;

#endregion

namespace NetParty.Application.IntegrationTests.CredentialsNS.CredentialsRepository.Helpers
    {
    public class MemoryStorageProvider : IStorageProvider
        {
        private readonly DeferredDisposalMemoryStream m_storage = new DeferredDisposalMemoryStream();

        public Stream GetStorage()
            {
            m_storage.Seek(0, SeekOrigin.Begin);
            return m_storage;
            }
        }
    }
