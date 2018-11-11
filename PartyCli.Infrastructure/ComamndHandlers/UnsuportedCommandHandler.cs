using PartyCli.Core.Enums;
using PartyCli.Core.Interfaces;
using PartyCli.Infrastructure.Exeptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PartyCli.Infrastructure.ComamndHandlers
{
    public class UnsuportedCommandHandler : ICommandHandler
    {
        public AveilableCommands Command { get => AveilableCommands.unsuported; }

        public UnsuportedCommandHandler()
        {
        }

        public void Handle(string[] args)
        {
            var suportedCommands = "";

            foreach (AveilableCommands comamnd in Enum.GetValues(typeof(AveilableCommands)))
            {
                if (comamnd != AveilableCommands.unsuported)
                    suportedCommands += $"{comamnd.ToString()}\r\n";
            }

            throw new PresentableExeption("Unsuported comand.\r\n" +
                                          "Supported commands:\r\n" +
                                          suportedCommands);
        }
    }
}
