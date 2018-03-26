using Newtonsoft.Json;
using PartyCli.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PartyCli.Repositories
{
    public class FileRepository<T> : IRepository<T>
    {
        private string path;

        public FileRepository()
        {
            string directory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Data");
            Directory.CreateDirectory(directory);
            path = Path.Combine(directory, $"{typeof(T).Name}.json");
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (List<T>)serializer.Deserialize(reader, typeof(List<T>));
                }
            }
            catch (IOException ex)
            {
                if (ex is FileNotFoundException || ex is DirectoryNotFoundException)
                {
                    return Enumerable.Empty<T>();
                }
                throw;
            }
        }

        public void Update(IEnumerable<T> servers)
        {
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, servers);
            }
        }
    }
}
