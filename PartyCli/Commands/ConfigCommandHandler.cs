using CommandLine;
using PartyCli.Interfaces;
using PartyCli.Services.Interfaces;

namespace PartyCli.Commands
{
  [Verb("config", HelpText = "Store username and password for API authorization in the persistent data store")]
  public class ConfigOptions
  {
    [Option("username", Required = true, HelpText = "Username")]
    public string Username { get; set; }
    [Option("password", Required = true, HelpText = "Password")]
    public string Password { get; set; }
  }

  public class ConfigCommandHandler: ICommandHandler<ConfigOptions>
  {
    private readonly IServerManagementService _service;
    private readonly IConsoleWriter _consoleWriter;

    public ConfigCommandHandler(IServerManagementService service, IConsoleWriter consoleWriter)
    {
      _service = service;
      _consoleWriter = consoleWriter;
    }

    public int HandleAndReturnExitCode(ConfigOptions options)
    {
      _service.SaveCredentials(options.Username, options.Password);
      _consoleWriter.DisplayInfo("User credentials saved successfully");

      return 0;
    }    
  }
}
