using NetParty.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetParty.Application.CommandExecutor
{
    public interface ICommandFacade
    {
        ICommand Create(ICommandArgs args);
        IEnumerable<ICommand> GetSupportedCommands();
    }
}