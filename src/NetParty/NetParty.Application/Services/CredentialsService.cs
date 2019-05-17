using NetParty.Application.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NetParty.Application
{
    public class CredentialsService : ICredentialsService, IDisposable
    {
        private const string FileName = "Secrets.sec";
        private readonly string SecretFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), FileName);
        private const string SecretStoreFormat = "u:{0}>>>s:{1}>>e";

        public async Task SaveCredentialsAsync(string userName, string userPassword)
        {
            var userInfo = string.Format(SecretStoreFormat, userName, userPassword);
            var secretUserInfo = ProtectedData.Protect(userInfo.ToByteArray(), null, DataProtectionScope.CurrentUser);

            using (FileStream sourceStream = new FileStream(SecretFilePath,
              FileMode.Append, FileAccess.Write, FileShare.None,
              bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(secretUserInfo, 0, secretUserInfo.Length);
            }
        }

        public void Dispose()
        {
            File.Delete(SecretFilePath);
        }
    }
}
