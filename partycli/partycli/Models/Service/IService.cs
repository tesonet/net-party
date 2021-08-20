using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace partycli.Models.Service
{
    public interface IService
    {
        GetServersListResponse GetServersList(Entities.GetServersInfo args);
        SaveUsersResponse SaveCredentials(Entities.Credentials args);

    }
}