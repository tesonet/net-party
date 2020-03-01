using NetParty.Model.Entities;
using System.Collections.Generic;

namespace NetParty.Model.Repositories
{
    public interface IRepository
    {
        Credentials GetCredentials();
        void SaveCredentials(Credentials args);
        List<Server> GetServersList();
        void SaveServerList(List<Server> list);
    }
}