using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using net_party.Services;
using net_party.Services.Contracts;
using System.Threading.Tasks;

namespace net_party
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IAuthService, AuthService>()
                .AddSingleton<IServerService, ServerService>()
                .AddSingleton(provider => new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build())
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            var authService = serviceProvider.GetService<IAuthService>();
            await authService.AcquireNewToken();

            logger.LogDebug("All done!");
        }
    }
}