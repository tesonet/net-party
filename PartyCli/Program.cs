using Castle.Core.Logging;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CommandLine;
using PartyCli.Interfaces;
using PartyCli.Repositories.Installers;
using PartyCli.Services.Installers;
using PartyCli.Services.Interfaces;
using PartyCli.WebApiClient.Installers;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace PartyCli
{
  class Program
  {
    #region Command line options
    [Verb("config", HelpText = "Store username and password for API authorization in the persistent data store")]
    class ConfigOptions
    {
      [Option("username", Required = true, HelpText = "Username")]
      public string Username { get; set; }
      [Option("password", Required = true, HelpText = "Password")]
      public string Password { get; set; }
    }

    [Verb("server_list", HelpText = "Fetch servers from API, store them in persistent data store")]
    class ServerOptions
    {
      [Option("local", Required = false, HelpText = "Fetch servers from persistent data store")]
      public bool Local { get; set; }
    }
    #endregion

    private static WindsorContainer _container = new WindsorContainer();

    [Browsable(false)]
    static void Main(string[] args)
    {
      _container.Install(FromAssembly.This(),
         new RepositoryInstaller(),
         new WebApiClientInstaller(),
         new ServicetInstaller()
         );

      var logger = _container.Resolve<ILogger>();
      var writer = _container.Resolve<IConsoleWriter>();      
      logger.Debug($"Starting PartyCli with args: {string.Join(" ", args) }");

      try
      {        
       Parser.Default.ParseArguments<ConfigOptions, ServerOptions>(args)
           .MapResult(
            (ConfigOptions opts) => RunConfigAndReturnExitCode(opts),
            (ServerOptions opts) => RunServerListAndReturnExitCode(opts),       
            errs => 1);
      }
      catch (AggregateException aex)
      {
        foreach (var ex in aex.InnerExceptions)
        {
          writer.DisplayExceptionMessage(ex);
          logger.Error($"{ex.Message} \n {ex.StackTrace}");
        }
      }
      catch (Exception ex)
      {
        writer.DisplayExceptionMessage(ex);
        logger.Error($"{ex.Message} \n {ex.StackTrace}");      
        
      }
    }

    private static int RunConfigAndReturnExitCode(ConfigOptions options)
    {
      var service = _container.Resolve<IServerManagementService>();
      service.SaveCredentials(options.Username, options.Password);

      return 0;
    }

    private static int RunServerListAndReturnExitCode(ServerOptions options)
    {
      Task.Run(async () =>
      {
        var service = _container.Resolve<IServerManagementService>();
        var servers = await service.FetchServersAsync(options.Local);

        var writer = _container.Resolve<IConsoleWriter>();
        writer.DisplayServers(servers);

      }).Wait();

      return 0;
    }
  }
}
