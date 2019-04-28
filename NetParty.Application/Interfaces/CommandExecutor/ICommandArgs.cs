using System.Collections.Generic;

namespace NetParty.Application.Interfaces
{
    public interface ICommandArgs
    {
        string CommandName { get; set; }
        IDictionary<string, string> CommandParameters { get; set; }
    }
}