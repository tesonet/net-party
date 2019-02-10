using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyCli.Interfaces
{
  public interface ICommandHandler<T>
  {
    int HandleAndReturnExitCode(T options);
    
  }
}
