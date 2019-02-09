using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PartyCli.Services.Interfaces;

namespace PartyCli.Services.Installers
{
  public class ServicetInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(        
        Component.For<IServerManagementService>().ImplementedBy<ServerManagementService>().LifestyleTransient()
      );
    }
  }
}
