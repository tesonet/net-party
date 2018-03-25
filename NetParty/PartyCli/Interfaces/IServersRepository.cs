using PartyCli.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyCli.Interfaces
{
    public interface IServersRepository
    {
        IEnumerable<Server> GetAll();

        void Update(IEnumerable<Server> servers);
    }
}
