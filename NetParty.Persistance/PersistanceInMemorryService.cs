using NetParty.Application.Interfaces;
using NetParty.Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace NetParty.Persistance
{
    public class PersistanceInMemorryService : IPersistance
    {
        private string dumpFileCredentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "localCredentials.json");
        private string dumpFileServersPath = Path.Combine(Directory.GetCurrentDirectory(), "localServers.json");
        public async Task<Credentials> GetCredentials()
        {
            var results = new Credentials();

            if (File.Exists(dumpFileServersPath))
            {
                results = JsonConvert.DeserializeObject<Credentials>(File.ReadAllText(dumpFileCredentialsPath));
            }

            await Task.CompletedTask;
            return results;
        }
        public async Task SaveCredentials(Credentials credentials)
        {
            File.WriteAllText(dumpFileCredentialsPath, JsonConvert.SerializeObject(credentials));
            await Task.CompletedTask;
        }
        public async Task<IList<Server>> GetServers()
        {
            var results = new List<Server>();
            if (File.Exists(dumpFileServersPath))
            {
                results = JsonConvert.DeserializeObject<List<Server>>(File.ReadAllText(dumpFileServersPath));
            }
                
            await Task.CompletedTask;
            return results;
        }

        public async Task SaveServers(IList<Server> list)
        {
            File.WriteAllText(dumpFileServersPath, JsonConvert.SerializeObject(list));
            await Task.CompletedTask;
        }
    }
}
