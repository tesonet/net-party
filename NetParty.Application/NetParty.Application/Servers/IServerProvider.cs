#region Using

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace NetParty.Application.Servers
    {
    public interface IServerProvider
        {
        Task<IEnumerable<Server>> GetServersAsync();
        }
    }
