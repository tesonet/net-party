using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using net_party.Repositories;
using net_party.Repositories.Contracts;
using net_party.Services;
using net_party.Services.Contracts;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace net_party
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IAuthService, AuthService>()
                .AddSingleton<IServerService, ServerService>()
                .AddSingleton<IAuthTokenRepository, AuthTokenRepository>()
                .AddSingleton(provider => new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build());

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var config = scope.ServiceProvider.GetService<IConfigurationRoot>();

                services.AddSingleton(v =>
                {
                    return new SqlConnection(config["ConnectionStrings:DefaultConnection"]);
                });
            }

            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            //var authService = serviceProvider.GetService<IAuthService>();
            //await authService.AcquireNewToken();
            var authTokenRepository = serviceProvider.GetService<IAuthTokenRepository>();
            await authTokenRepository.Get();

            logger.LogDebug("All done!");
        }
    }
}