using NetParty.Application.Interfaces;
using System.Collections.Generic;

namespace NetParty.Application.Tests.Infrastucture
{
    public class SubCommand : ICommandArgs
    {
        public string CommandName { get; set; }
        public IDictionary<string, string> CommandParameters { get; set; }
    }
}
