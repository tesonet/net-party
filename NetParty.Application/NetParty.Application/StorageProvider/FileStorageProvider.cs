#region Using

using System.IO;
using NetParty.Application.CredentialsNS;
using NetParty.Application.Servers;

#endregion

namespace NetParty.Application.StorageProvider
    {
    public class FileStorageProvider : ICredentialsStorageProvider, IServerStorageProvider
        {
        private readonly string m_filePath;

        public FileStorageProvider(string filePath)
            {
            m_filePath = filePath;
            }

        public Stream GetStorage() => new FileStream(m_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

        public void ClearStorage()
            {
            if (File.Exists(m_filePath))
                File.Delete(m_filePath);
            }
        }
    }
