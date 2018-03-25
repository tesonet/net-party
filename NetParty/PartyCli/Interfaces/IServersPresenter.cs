using PartyCli.Models;
using System.Collections.Generic;

namespace PartyCli.Interfaces
{
    public interface IServersPresenter
    {
        void Display(IEnumerable<Server> servers);
    }
}
