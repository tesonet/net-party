
using System.Collections.Generic;
using partycli4.Data;

namespace partycli4.Interface
{
    public interface IDataRepository
    {
        User GetUser();
        IList<Server> GetServerList();
        void SaveUser(User user);
        void SaveServerList(IList<Server> serverList);
    }
}
