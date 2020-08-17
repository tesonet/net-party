using System.IO;
using System.Threading.Tasks;

namespace NetParty.Data.Files
{
    public class RawFileRepository : IRepository<byte[]>
    {
        private readonly string _filePath;

        public RawFileRepository(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<byte[]> GetAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new byte[0];
            }

            using (var stream = File.OpenRead(_filePath))
            {
                var result = new byte[stream.Length];

                await stream.ReadAsync(result, 0, (int)stream.Length);

                return result;
            }
        }

        public async Task SaveAsync(byte[] item)
        {
            using (var stream = File.OpenWrite(_filePath))
            {
                await stream.WriteAsync(item, 0, item.Length);
            }
        }
    }
}
