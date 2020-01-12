using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using net_party.Entities.Database;
using net_party.Repositories;
using net_party.Repositories.Contracts;
using net_party.Services;
using net_party.Services.Contracts;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace net_party
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddLogging()
                // Repositories
                .AddScoped<IAuthTokenRepository, AuthTokenRepository>()
                .AddScoped<ICredentialRepository, CredentialRepository>()
                .AddScoped<IServerRepository, ServerRepository>()

                // Services
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IServerService, ServerService>()
                .AddScoped<IPasswordService, PasswordService>()
                .AddSingleton<IRngService, RngService>()
                .AddSingleton<RNGCryptoServiceProvider>()
                .AddSingleton(provider => new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build());

            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var config = scope.ServiceProvider.GetService<IConfigurationRoot>();

                services.AddTransient(v =>
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
            var token = await authTokenRepository.Get();
            var serverService = serviceProvider.GetService<IServerService>();
            var servers = await serverService.GetServers();
            var localServers = await serverService.GetServers(true);

            foreach (var server in servers)
            {
                Console.WriteLine($"{server.Name} {server.Distance}");
            }

            foreach (var localServer in localServers)
            {
                Console.WriteLine($"{localServer.Name} {localServer.Distance}");
            }
            //var credentialRepository = serviceProvider.GetService<ICredentialRepository>();
            //var passwordService = serviceProvider.GetService<IPasswordService>();
            //var hashed = passwordService.HashPassword("partyanimal");
            //var user = await credentialRepository.Get("tesonet");
            //var validated = passwordService.ValidatePassword("partyanimal", user.Password);
            //var result = await credentialRepository.Add(new Credential() { Username = "tesonet", Password = hashed}) ;

            logger.LogDebug("All done!");
            Console.ReadLine();
        }
    }
}