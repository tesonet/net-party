using Newtonsoft.Json;
using PartyCli.Interfaces;
using PartyCli.Models;
using PartyCli.Properties;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace PartyCli.Repositories
{
    public class CredentialsRepository : ICredentialsRepository
    {
        private string path;

        public CredentialsRepository()
        {
            string directory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            path = Path.Combine(directory, Settings.Default.CredentialsFileName);
        }

        public Credentials Get()
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (Credentials)serializer.Deserialize(reader, typeof(Credentials));
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

        public void Update(Credentials model)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, model);
            }
        }
    }
}
