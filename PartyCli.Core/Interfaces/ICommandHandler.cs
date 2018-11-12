using PartyCli.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Core.Interfaces
{
    public interface ICommandHandler
    {
        AveilableCommands Command { get; }

        void Handle(string[] args);
    }
}
