using Castle.Core.Logging;
using PartyCli.Entities;
using PartyCli.Interfaces;
using System;
using System.Collections.Generic;

namespace PartyCli.Infrastructure
{
  public class ConsoleWriter : IConsoleWriter
  {
    private readonly ILogger _logger;

    public ConsoleWriter(ILogger logger)
    {
      _logger = logger;
    }

    public void DisplayServers(IEnumerable<Server> servers, string sourceName)
    {
      var count = 0;
      Console.ForegroundColor = ConsoleColor.Green;
      try
      {
        Console.WriteLine($"List of Servers from {sourceName}:");
        foreach (var server in servers)
        {
          count++;
          Console.WriteLine($"{count}. Server name: {server.Name}");
        }
        Console.WriteLine($"Total servers fetched: {count}");        
      }
      finally
      {
        Console.ResetColor();
        _logger.Info($"Servers displayed: {count} ");
      }
    }

    public void DisplayExceptionMessage(Exception ex)
    {
      Console.ForegroundColor = ConsoleColor.Red;
      try
      {
        Console.WriteLine($"Error: {ex.Message}");
      }
      finally
      {
        Console.ResetColor();
      }
    }

    public void DisplayInfo(string infoLine)
    {
      Console.WriteLine(infoLine);
    }
  }
}
