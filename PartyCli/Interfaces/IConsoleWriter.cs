using PartyCli.Entities;
using System;
using System.Collections.Generic;

namespace PartyCli.Interfaces
{
  public interface IConsoleWriter
  {
    void DisplayServers(IEnumerable<Server> servers);
    void DisplayExceptionMessage(Exception ex);
  }
}
