#region Using

using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace NetParty.Application.CredentialsNS
    {
    public class CredentialsRepository : ICredentialsRepository
        {
        private readonly IStorageProvider m_storageProvider;

        public CredentialsRepository(IStorageProvider storageProvider)
            {
            m_storageProvider = storageProvider;
            }

        public Task StoreAsync(Credentials credentials)
            {
            using (var storage = m_storageProvider.GetStorage())
            using (StreamWriter streamWriter = new StreamWriter(storage))
                {
                var serializedCredentials = JsonConvert.SerializeObject(credentials);
                return streamWriter.WriteAsync(serializedCredentials);
                }
            }

        public async Task<Credentials> LoadAsync()
            {
            using (var storage = m_storageProvider.GetStorage())
                {
                string serializedCredentials;
                using (StreamReader streamReader = new StreamReader(storage))
                    {
                    serializedCredentials = await streamReader.ReadToEndAsync();
                    }

                var credentials = JsonConvert.DeserializeObject<Credentials>(serializedCredentials);
                return credentials;
                }
            }
        }
    }
