using System.Collections.Generic;
using NetParty.Application.Interfaces;

namespace partycli
{
    internal class ConsoleCommand : ICommandArgs
    {
        public string CommandName { get; set; }
        public IDictionary<string, string> CommandParameters { get; set; }
    }
}