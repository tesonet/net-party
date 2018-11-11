using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Core.Interfaces
{
    public interface IReceiver
    {
        bool Accept(string[] args);
    }
}
