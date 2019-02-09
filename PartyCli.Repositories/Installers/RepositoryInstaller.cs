using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using PartyCli.Entities;
using PartyCli.Extentions;
using PartyCli.Repositories.Interfaces;
using PartyCli.Repositories.JsonFileStorage;
using PartyCli.Repositories.LiteDbStorage;

namespace PartyCli.Repositories.Installers
{
  public class RepositoryInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(
        Component.For<IRepositorySettings>().AsAppSettingsAdapter().LifestyleSingleton());

      var settings = container.Resolve<IRepositorySettings>();

      switch (settings.StorageType)
      {
        case StorageType.JsonFile:
          container.Register(
          Component.For<IRepository<Credentials>>().ImplementedBy<CredentialsJsonFileRepository>().LifestyleTransient(),
          Component.For<IRepository<Server>>().ImplementedBy<ServerJsonFileRepository>().LifestyleTransient()
          );
          break;
        default:
          container.Register(
            Component.For<IRepository<Credentials>>().ImplementedBy<CredentialsLiteDbRepository>().LifestyleTransient(),
            Component.For<IRepository<Server>>().ImplementedBy<ServerLiteDbRepository>().LifestyleTransient()
           );
          break;
      };
    }
  }
}
