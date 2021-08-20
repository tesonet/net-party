using partycli.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Models.Reporsitory
{
     public interface IRepository
    {
        Credentials GetUserInfo();
        void SaveUserInfo(Credentials args);
        List<ServerList> GetServers();
        void SaveServers(List<ServerList> list);
    }
}
