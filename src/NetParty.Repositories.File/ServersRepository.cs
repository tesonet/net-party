using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using GuardNet;
using NetParty.Contracts;
using NetParty.Repositories.Core;
using NetParty.Utils;

namespace NetParty.Repositories.File
{
    public class ServersRepository : IServersRepository
    {
        private const string FileName = "Servers.data";
        private readonly string _filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);

        public async Task<ServerDto[]> GetServersAsync()
        {
            byte[] buffer;

            using (FileStream sourceStream = new FileStream(_filePath,
                FileMode.Open, FileAccess.Read, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                buffer = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(buffer, 0, (int)sourceStream.Length);
            }

            var data = buffer.ToObjectType<ServerDto[]>();

            return data;
        }

        public async Task<ServerDto[]> SaveServersAsync(ServerDto[] data)
        {
            Guard.NotNull(data, nameof(data));

            var dataBytes = data.ToByteArray();

            using (FileStream sourceStream = new FileStream(_filePath,
             FileMode.Append, FileAccess.Write, FileShare.None,
             bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(dataBytes, 0, dataBytes.Length);
            }

            return data;
        }
    }
}
