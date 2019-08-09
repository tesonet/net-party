using System.Threading.Tasks;
using System.IO;
using System.IO.Abstractions;
using System.Text;

namespace partycli.Repository
{
    public class FileRepositoryProvider : IRepositoryProvider
    {
        private readonly string m_filePath;
        private readonly IFileSystem m_fileSystem;

        public FileRepositoryProvider(string filePath, IFileSystem fileSystem)
        {
            m_filePath = filePath;
            m_fileSystem = fileSystem;
        }

        public FileRepositoryProvider(string filePath) : this(filePath, fileSystem: new FileSystem())
        {
            m_filePath = filePath;
        }

        public void Reset()
        {
            if (m_fileSystem.File.Exists(m_filePath))
            {
                m_fileSystem.File.Delete(m_filePath);
            }
        }

        public async Task SaveAsync(string content)
        {
            var buffer = Encoding.UTF8.GetBytes(content);

            using (var fs = m_fileSystem.FileStream.Create(m_filePath, FileMode.OpenOrCreate,
                FileAccess.Write, FileShare.None, buffer.Length, true))
            {
                await fs.WriteAsync(buffer, 0, buffer.Length);
            }
        }

        public async Task<string> LoadAsync()
        {
            using (var reader = m_fileSystem.File.OpenText(m_filePath))
            {
                var content = await reader.ReadToEndAsync();
                return content;
            }
        }
    }
}
