using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PartyCli.Extentions;
using PartyCli.WebApiClient.Interfaces;

namespace PartyCli.WebApiClient.Installers
{
  public class WebApiClientInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(
        Component.For<IWebApiClientSettings>().AsAppSettingsAdapter().LifestyleSingleton(),
        Component.For<IWebApiClient>().ImplementedBy<WebApiClient>().LifestyleTransient()
      );
    }
  }
}
