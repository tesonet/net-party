using System.Collections.Generic;

namespace PartyCLI
{
    public interface IDataFilesManager
    {
        void SaveUserData(UserData user);
        UserData GetUserData();

        void SaveServersList(List<Server> serversList);
        List<Server> GetServersList();
    }
}
