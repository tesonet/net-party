using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace PartyCLI
{
    public class DataFilesManager : IDataFilesManager
    {
        private string defaultPath = AppDomain.CurrentDomain.BaseDirectory + "/DataFiles/";
        private string userDataStorage = "UserData.json";
        private string serversListStorage = "ServersList.json";

        public DataFilesManager()
        {
            if (!Directory.Exists(defaultPath))
                Directory.CreateDirectory(defaultPath);
        }

        //
        // Saving methods
        //
        public void SaveUserData(UserData user)
        {
            string path = defaultPath + userDataStorage;
            SaveSerializedObjectToFile(path, user);
        }
        public void SaveServersList(List<Server> servers)
        {
            string path = defaultPath + serversListStorage;
            SaveSerializedObjectToFile(path, servers);
        }

        //
        // Getting methods
        //
        public UserData GetUserData()
        {
            UserData user = GetDeserializedObjectFromFile<UserData>(defaultPath + userDataStorage);
            return user;
        }
        public List<Server> GetServersList()
        {
            List<Server> servers = GetDeserializedObjectFromFile<List<Server>>(defaultPath + serversListStorage);
            return servers;
        }

        //
        // Generic saver & geter
        //
        private T GetDeserializedObjectFromFile<T>(string path)
        {
            if (!File.Exists(path))
                return default(T);

            var fileContents = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(fileContents);
        }

        private void SaveSerializedObjectToFile<T>(string path, T obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(path, json);
        }
    }
}
