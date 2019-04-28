using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetParty.Application.Interfaces
{
    public interface ICommandsExecutor
    {
        Task<IEnumerable<ICommandResult>> Execute(IEnumerable<ICommandArgs> parameters);
    }
}
