using Castle.Facilities.Logging;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Services.Logging.NLogIntegration;
using Castle.Windsor;
using PartyCli.Commands;
using PartyCli.Infrastructure;
using PartyCli.Interfaces;

namespace PartyCli.Installers
{
  public class DefaultInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.AddFacility<TypedFactoryFacility>();
      container.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>());

      container.Register(
          Component.For<IConsoleWriter>().ImplementedBy<ConsoleWriter>().LifestyleTransient(),
          Component.For<ICommandHandler<ConfigOptions>>().ImplementedBy<ConfigCommandHandler>().LifestyleTransient(),
          Component.For<ICommandHandler<ServerListOptions>>().ImplementedBy<ServerListCommandHandler>().LifestyleTransient()
          );
    }
  }  
}
