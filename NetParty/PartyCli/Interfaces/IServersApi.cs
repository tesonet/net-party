using PartyCli.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PartyCli.Interfaces
{
    public interface IServersApi
    {
        Task AuthorizeAsync(Credentials credentials);

        Task<IEnumerable<Server>> GetServersAsync();
    }
}
