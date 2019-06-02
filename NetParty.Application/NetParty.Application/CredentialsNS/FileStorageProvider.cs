#region Using

using System.IO;

#endregion

namespace NetParty.Application.CredentialsNS
    {
    public class FileStorageProvider : IStorageProvider
        {
        private readonly string m_filePath;

        public FileStorageProvider(string filePath)
            {
            m_filePath = filePath;
            }

        public Stream GetStorage() => new FileStream(m_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        }
    }
