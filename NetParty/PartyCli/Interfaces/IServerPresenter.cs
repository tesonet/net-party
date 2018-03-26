using PartyCli.Models;
using System.Collections.Generic;

namespace PartyCli.Interfaces
{
    public interface IServerPresenter
    {
        void Display(IEnumerable<Server> servers);
    }
}
