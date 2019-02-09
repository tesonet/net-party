using PartyCli.WebApiClient.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyCli.WebApiClient.Interfaces
{
  public interface IWebApiClient
  {
    Task<ICollection<ServerDataContract>> GetServersAsync(string token);
    Task<TokenDataContract> GetTokenAsync(string username, string password);
  }
}
