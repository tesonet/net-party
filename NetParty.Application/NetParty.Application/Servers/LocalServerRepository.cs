#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace NetParty.Application.Servers
    {
    public class LocalServerRepository : IServerRepository
        {
        private readonly IServerStorageProvider m_storageProvider;

        public LocalServerRepository(IServerStorageProvider storageProvider)
            {
            m_storageProvider = storageProvider ?? throw new ArgumentNullException(nameof(storageProvider));
            }

        public async Task<IEnumerable<Server>> GetServersAsync()
            {
            using (var storage = m_storageProvider.GetStorage())
                {
                string serializedServers;
                using (StreamReader streamReader = new StreamReader(storage))
                    {
                    serializedServers = await streamReader.ReadToEndAsync();
                    }

                var servers = JsonConvert.DeserializeObject<IEnumerable<Server>>(serializedServers);
                return servers;
                }
            }

        public async Task StoreServersAsync(IEnumerable<Server> servers)
            {
            m_storageProvider.ClearStorage();
            using (var storage = m_storageProvider.GetStorage())
            using (StreamWriter streamWriter = new StreamWriter(storage))
                {
                var serializedServers = JsonConvert.SerializeObject(servers);
                await streamWriter.WriteAsync(serializedServers);
                }
            }
        }
    }
