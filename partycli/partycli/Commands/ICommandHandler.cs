using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using partycli.core.Execution;

namespace partycli.Commands
{
    interface ICommandHandler<T> where T : ICommand
    {
        void Handle(T command);
    }
}
