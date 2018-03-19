using System;
using System.Collections.Generic;
using System.IO;

namespace Tesonet
{
    public class FileService : IFileService
    {
        public string[] ReadLocalServers()
        {
            return File.ReadAllLines(Constants.ServersFile);
        }

        public string[] ReadUserData()
        {
            return File.ReadAllLines(Constants.UserInfoFile);
        }

        public void WriteServerData(List<Server> servers)
        {
            using (var file = new StreamWriter(Constants.ServersFile))
            {
                servers.ForEach(v => file.WriteLine(v.Name));
            }
        }

        public void WriteUserData(string username, string password)
        {
            File.WriteAllText(Constants.UserInfoFile, username + Environment.NewLine + password);
        }
    }
}
