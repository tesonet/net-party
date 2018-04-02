using Newtonsoft.Json;
using PartyCli.Interfaces;
using PartyCli.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PartyCli.Repositories
{
    public class FileRepository
    {
        protected static string directoryPath;

        public static void Configure(FileRepositoryOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            directoryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), options.FolderName);
            Directory.CreateDirectory(directoryPath);
        }
    }

    public class FileRepository<T> : FileRepository, IRepository<T>
    {
        private string GetPath()
        {
            return Path.Combine(directoryPath, $"{typeof(T).Name}.json");
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                using (var reader = new StreamReader(GetPath(), Encoding.UTF8))
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
            using (var writer = new StreamWriter(GetPath(), false, Encoding.UTF8))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(writer, servers);
            }
        }
    }
}
