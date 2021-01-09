namespace Tesonet.ServerListApp.Infrastructure.Configuration
{
    using System.Threading.Tasks;

    public interface IPersistentConfiguration
    {
        T GetSection<T>(string section) where T : class, new();

        Task Save<T>(T obj) where T : class;
    }
}