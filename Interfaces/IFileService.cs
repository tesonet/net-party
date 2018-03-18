using System.Collections.Generic;

namespace Tesonet
{
    public interface IFileService
    {
        void WriteUserData(string username, string password);

        string[] ReadUserData();

        void WriteServerData(List<Server> servers);

        string[] ReadLocalServers();

    }
}
