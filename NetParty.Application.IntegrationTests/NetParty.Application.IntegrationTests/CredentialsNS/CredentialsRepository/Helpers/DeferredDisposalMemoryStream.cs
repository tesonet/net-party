#region Using

using System.IO;

#endregion

namespace NetParty.Application.IntegrationTests.CredentialsNS.CredentialsRepository.Helpers
    {
    public class DeferredDisposalMemoryStream : MemoryStream
        {
        protected override void Dispose(bool disposing)
            {
            // do nothing - it will persist until GC gets to it, instead of being closed when disposed
            }
        }
    }
