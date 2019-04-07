using Newtonsoft.Json;
using partycli.core.Repositories.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.core.Repositories.Storage
{
    public class JsonStorageManager : IStorageManager
    {
        public void SaveCredentials(Credentials credentials)
        {
            using (TextWriter writer = new StreamWriter("./creds.json", false))
                writer.Write(JsonConvert.SerializeObject(credentials));
        }

        public IEnumerable<Server> GetServers()
        {
            IEnumerable<Server> servers = null;
            using (TextReader reader = new StreamReader("./servers.json"))
            {
                string json = reader.ReadToEnd();
                servers = JsonConvert.DeserializeObject<IEnumerable<Server>>(json);
            }

            return servers;
        }

        public void StoreServers(IEnumerable<Server> servers)
        {
            using (TextWriter writer = new StreamWriter("./servers.json", false))
                writer.Write(JsonConvert.SerializeObject(servers));
        }

        public Credentials GetCredentials()
        {
            Credentials credentials = null;
            using (TextReader reader = new StreamReader("./creds.json"))
            {
                string json = reader.ReadToEnd();
                credentials = JsonConvert.DeserializeObject<Credentials>(json);
            }

            return credentials;
        }
    }
}
