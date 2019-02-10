using CommandLine;
using PartyCli.Interfaces;
using PartyCli.Services.Interfaces;

namespace PartyCli.Commands
{
  [Verb("server_list", HelpText = "Fetch servers from API, store them in local data storage")]
  public class ServerListOptions
  {
    [Option("local", Required = false, HelpText = "Fetch servers from local data storage")]
    public bool Local { get; set; }

    [Option("clear", Required = false, HelpText = "Clear servers from local data storage")]
    public bool Clear { get; set; }
  }

  public class ServerListCommandHandler : ICommandHandler<ServerListOptions>
  {
    private readonly IServerManagementService _service;
    private readonly IConsoleWriter _consoleWriter;

    public ServerListCommandHandler(IServerManagementService service, IConsoleWriter consoleWriter)
    {
      _service = service;
      _consoleWriter = consoleWriter;
    }

    public int HandleAndReturnExitCode(ServerListOptions options)
    {
      if (options.Clear)
      {
        _service.ClearServers();
        _consoleWriter.DisplayInfo("Server list cleared from local data storage");
      }
      else
      {
        var servers = _service.FetchServersAsync(options.Local).Result;
        _consoleWriter.DisplayServers(servers, options.Local ? "local data storage" : "WebApi");
      }
      return 0;
    }
  }
}
