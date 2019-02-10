using PartyCli.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyCli.Services.Interfaces
{
  public interface IServerManagementService
  {
    Task<ICollection<Server>> FetchServersAsync(bool local);
    void SaveCredentials(string username, string password);
    void SaveServers(IEnumerable<Server> servers);
    void ClearServers();
  }
}
