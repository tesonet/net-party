using log4net;
using Newtonsoft.Json;
using partycli.core.Repositories.Model;
using System.Collections.Generic;
using System.IO;

namespace partycli.core.Repositories.Storage
{
    public class JsonStorageManager : IStorageManager
    {
        const string StorageSettingsLocation = "./StorageSettings.json";

        StorageSettings _settings { get; set; }
        readonly ILog _logger;

        public JsonStorageManager()
        {
            //Default settings
            _settings = new StorageSettings();
            _logger = LogManager.GetLogger(GetType());
            Init();
        }

        void Init()
        {
            try
            {
                //Try to read from file
                using (TextReader reader = new StreamReader(StorageSettingsLocation))
                {
                    _settings = JsonConvert.DeserializeObject<StorageSettings>(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                _logger.Warn("Storage settings not found. Using default settings.");
            }
        }

        public void SaveCredentials(Credentials credentials)
        {
            try
            {
                using (TextWriter writer = new StreamWriter(_settings.CredentialsSavePath, false))
                    writer.Write(JsonConvert.SerializeObject(credentials));
            }
            catch (DirectoryNotFoundException)
            {
                _logger.Error($"Credentials save directory [{Path.GetFullPath(Path.GetDirectoryName(_settings.ServerSavePath))}] does not exist.");
                throw;
            }
        }

        public IEnumerable<Server> GetServers()
        {
            IEnumerable<Server> servers = null;
            try
            {
                using (TextReader reader = new StreamReader(_settings.ServerSavePath))
                {
                    servers = JsonConvert.DeserializeObject<IEnumerable<Server>>(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                _logger.Error("Server list file not found.");
                throw;
            }

            return servers;
        }

        public void StoreServers(IEnumerable<Server> servers)
        {
            try
            {
                using (TextWriter writer = new StreamWriter(_settings.ServerSavePath, false))
                    writer.Write(JsonConvert.SerializeObject(servers));
            }
            catch (DirectoryNotFoundException)
            {
                _logger.Error($"Server list save directory [{Path.GetFullPath(Path.GetDirectoryName(_settings.ServerSavePath))}] does not exist.");
                throw;
            }
        }

        public Credentials GetCredentials()
        {
            Credentials credentials = null;
            try
            {
                using (TextReader reader = new StreamReader(_settings.CredentialsSavePath))
                {
                    credentials = JsonConvert.DeserializeObject<Credentials>(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                _logger.Error("Credentials file not found. ");
                throw;
            }

            return credentials;
        }
    }
}
