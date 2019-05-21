using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;
using GuardNet;
using NetParty.Contracts;
using NetParty.Repositories.Core;
using NetParty.Utils;

namespace NetParty.Repositories.File
{
    public class CredentialsRepository : ICredentialsRepository, IDisposable
    {
        private const string FileName = "Secrets.sec";
        private readonly string _filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);

        public async Task SaveCredentialsAsync(Credentials credentials)
        {
            Guard.NotNull(credentials, nameof(credentials));

            var secretUserInfo = ProtectedData.Protect(credentials.ToByteArray(), null, DataProtectionScope.CurrentUser);

            using (FileStream sourceStream = new FileStream(_filePath,
              FileMode.Append, FileAccess.Write, FileShare.None,
              bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(secretUserInfo, 0, secretUserInfo.Length);
            }
        }

        public async Task<Credentials> GetCredentialsAsync()
        {
            byte[] buffer;

            using (FileStream sourceStream = new FileStream(_filePath,
                FileMode.Open, FileAccess.Read, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                buffer = new byte[sourceStream.Length];
                await sourceStream.ReadAsync(buffer, 0, (int)sourceStream.Length);
            }

            var unprotectedData = ProtectedData.Unprotect(buffer, null, DataProtectionScope.CurrentUser);
            var credentials = unprotectedData.ToObjectType<Credentials>();

            return credentials;
        }

        public void Dispose()
        {
            System.IO.File.Delete(_filePath);
        }
    }
}
