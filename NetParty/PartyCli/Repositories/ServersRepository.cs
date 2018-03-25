using Newtonsoft.Json;
using PartyCli.Interfaces;
using PartyCli.Models;
using PartyCli.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PartyCli.Repositories
{
    public class ServersRepository : IServersRepository
    {
        private string path;

        public ServersRepository()
        {
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path = Path.Combine(directory, Settings.Default.ServersFileName);
        }

        public IEnumerable<Server> GetAll()
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (List<Server>)serializer.Deserialize(reader, typeof(List<Server>));
                }
            }
            catch (FileNotFoundException)
            {
                return Enumerable.Empty<Server>();
            }
        }

        public void Update(IEnumerable<Server> servers)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, servers);
            }
        }
    }
}
