using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NetParty.Data;
using Newtonsoft.Json;

namespace NetParty.Domain.User
{
    public class EncryptedCredentialService : ICredentialService
    {
        private readonly byte[] _entropy = { 9, 8, 12, 90, 5, 67 };
        private readonly IRepository<byte[]> _repository;

        public EncryptedCredentialService(IRepository<byte[]> repository)
        {
            _repository = repository;
        }

        public async Task<Credentials> GetAsync()
        {
            var result = await _repository.GetAsync();

            return result.Length > 0 ? JsonConvert.DeserializeObject<Credentials>(Decrypt(result)) : null;
        }

        public async Task SaveAsync(Credentials credentials)
        {
            await _repository.SaveAsync(Encrypt(JsonConvert.SerializeObject(credentials)));
        }

        private byte[] Encrypt(string value)
        {
            return ProtectedData.Protect(
                Encoding.UTF8.GetBytes(value),
                _entropy,
                DataProtectionScope.CurrentUser
                );
        }

        private string Decrypt(byte[] value)
        {
            return Encoding.UTF8.GetString(
                ProtectedData.Unprotect(
                    value,
                    _entropy,
                    DataProtectionScope.CurrentUser
                    )
                );
        }
    }
}
