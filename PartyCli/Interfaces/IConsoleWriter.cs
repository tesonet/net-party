using PartyCli.Entities;
using System;
using System.Collections.Generic;

namespace PartyCli.Interfaces
{
  public interface IConsoleWriter
  {
    void DisplayInfo(string infoLine);
    void DisplayServers(IEnumerable<Server> servers, string sourceName);
    void DisplayExceptionMessage(Exception ex);
  }
}
