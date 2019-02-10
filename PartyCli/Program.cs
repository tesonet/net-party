using Castle.Core.Logging;
using Castle.Windsor;
using Castle.Windsor.Installer;
using CommandLine;
using PartyCli.Commands;
using PartyCli.Interfaces;
using PartyCli.Repositories.Installers;
using PartyCli.Services.Installers;
using PartyCli.WebApiClient.Installers;
using System;

namespace PartyCli
{
  class Program
  {    
    static int Main(string[] args)
    {
      int exitCode = 1;

      var container = new WindsorContainer();

      container.Install(FromAssembly.This(),
         new RepositoryInstaller(),
         new WebApiClientInstaller(),
         new ServicetInstaller()
         );

      var logger = container.Resolve<ILogger>();

      logger.Debug($"Starting PartyCli with args: {string.Join(" ", args) }");

      var consoleWriter = container.Resolve<IConsoleWriter>();
      var configHandler = container.Resolve<ICommandHandler<ConfigOptions>>();
      var serverListHandler = container.Resolve<ICommandHandler<ServerListOptions>>();
  
      try
      {
        exitCode = Parser.Default.ParseArguments<ConfigOptions, ServerListOptions>(args)
          .MapResult(
          (ConfigOptions opts) => configHandler.HandleAndReturnExitCode(opts),
          (ServerListOptions opts) => serverListHandler.HandleAndReturnExitCode(opts),
          errs => 1);
      }
      catch (AggregateException aex)
      {
        foreach (var ex in aex.InnerExceptions)
        {
          consoleWriter.DisplayExceptionMessage(ex);
          logger.Error($"{ex.Message} \n {ex.StackTrace}");
        }
      }
      catch (Exception ex)
      {
        consoleWriter.DisplayExceptionMessage(ex);
        logger.Error($"{ex.Message} \n {ex.StackTrace}");
      }

      return exitCode;
    }
  }
}
