using System.Configuration;
using Castle.Components.DictionaryAdapter;
using Castle.MicroKernel.Registration;

namespace PartyCli.Extentions
{
  public static class RegistrationExtensions
  {
    private static readonly DictionaryAdapterFactory DictionaryAdapterFactory = new DictionaryAdapterFactory();

    public static ComponentRegistration<T> AsAppSettingsAdapter<T>(this ComponentRegistration<T> componentRegistration) where T : class
    {
      componentRegistration.UsingFactoryMethod(
        (kernel, context) => (T)DictionaryAdapterFactory.GetAdapter(context.RequestedType, ConfigurationManager.AppSettings));

      return componentRegistration;
    }
  }
}
