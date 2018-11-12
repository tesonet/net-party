using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Core.Interfaces
{
    public interface ILogger
    {
        void Error(string entry);

        void Debug(string entry);
    }
}
