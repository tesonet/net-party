using PartyCli.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Core.Interfaces
{
    public interface IPresenter
    {
        void DisplayMessage(string message);

        void DisplayServers(List<Server> servers);

    }
}
