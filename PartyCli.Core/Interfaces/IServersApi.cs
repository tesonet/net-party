using PartyCli.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PartyCli.Core.Interfaces
{
    public interface IServersApi
    {
        Task<List<Server>> GetServersAsync();
    }
}
