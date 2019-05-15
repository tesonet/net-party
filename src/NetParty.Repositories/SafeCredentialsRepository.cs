using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using NetParty.Contracts;
using NetParty.Interfaces.Repositories;
using NetParty.Utils;

namespace NetParty.Repositories
{
    public class SafeCredentialsRepository : ICredentialsRepository
    {
        private readonly string _entropy;
        private const string CredentialsFile = "credentials.dat";

        public SafeCredentialsRepository(string entropy)
        {
            _entropy = entropy;
        }

        public SafeCredentialsRepository()
        {
            _entropy = null;
        }

        public Task SaveCredentials(Credentials credentials)
        {
            return Task.Run(() =>
            {
                var encryptedResult = ProtectedData.Protect(credentials.ToByteArray(), _entropy?.ToByteArray(),
                    DataProtectionScope.CurrentUser);
                File.WriteAllBytes(CredentialsFile, encryptedResult);
            });
        }

        public Task<Credentials> LoadCredentials()
        {
            Credentials result = null;
            if (File.Exists(CredentialsFile))
            {
                var encryptedResult = File.ReadAllBytes(CredentialsFile);
                result = ProtectedData
                    .Unprotect(encryptedResult, _entropy?.ToByteArray(), DataProtectionScope.CurrentUser)
                    .ToObject<Credentials>();
            }

            return Task.FromResult(result);
        }
    }
}